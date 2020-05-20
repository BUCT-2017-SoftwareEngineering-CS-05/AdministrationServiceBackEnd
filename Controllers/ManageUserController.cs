using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : Controller
    {
        //[Authorize]
        [HttpGet("GetUsers")]
        [HttpHead]
        public async Task<IActionResult> GetUser([FromQuery] UserDtoParameters parameters)
        {
            //if (!_propertyMappingService.ValidMappingExistsFor<CompanyDto, Company>(parameters.OrderBy))
            //{
            //    return BadRequest();
            //}

            //if (!_propertyCheckerService.TypeHasProperties<CompanyDto>(parameters.Fields))
            //{
            //    return BadRequest();
            //}
            MuseumContext _context = new MuseumContext();
            IManageUser _manageUser = new ManageUser(_context);
            var users = await _manageUser.GetUsersAsync(parameters);
            var paginationMetadata = new
            {
                totalCount = users.TotalCount,
                pageSize = users.PageSize,
                currentPage = users.CurrentPage,
                totalPages = users.TotalPages
            };
            //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata,
            //    new JsonSerializerOptions
            //    {
            //        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            //    }));

            //var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            //var shapedData = companyDtos.ShapeData(parameters.Fields);

            //var links = CreateLinksForCompany(parameters, companies.HasPrevious, companies.HasNext);

            // { value: [xxx], links }

            //var shapedCompaniesWithLinks = shapedData.Select(c =>
            //{
            //    var companyDict = c as IDictionary<string, object>;
            //    var companyLinks = CreateLinksForCompany((Guid)companyDict["Id"], null);
            //    companyDict.Add("links", companyLinks);
            //    return companyDict;
            //});

            //var linkedCollectionResource = new
            //{
            //    value = shapedCompaniesWithLinks,
            //    links
            //};
            return Ok(new { code=0,data=new{ items=users,total = users.TotalCount} });
        }
        // POST api/<controller>/Changepwd
        //[Authorize]
        [HttpPost("Changepwd")]
        public  IActionResult Changepwd([FromBody]User user)
        {
            //if (GetRolesFromAuthorizZation() < 0)
            //{
            //    return Json(new { code = -1, msg = "您没有权限修改用户密码！" });
            //}
            if(UserSystem.ChangePassword(user))
                return Json(new { code = 0, msg = "密码修改成功" });
            return Json(new { code = -1, msg = "密码修改失败" });
        }
        // DELETE api/<controller>/5
        //[Authorize]
        [HttpGet("Delete")]
        public IActionResult Delete([FromQuery]User user)
        {
           if(UserSystem.DeleteUser(user))
                return Json(new {code=0,msg="删除成功！" } );
            return Json(new { code = 0, msg = "删除失败！" });
        }
        private int GetRolesFromAuthorizZation()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst("Roles")?.Value);
        }
    }
}
