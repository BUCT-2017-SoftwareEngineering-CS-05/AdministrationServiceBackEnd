using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    public class ManageUserController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("test")]
        [HttpHead]
        public async Task<IActionResult> GetCompanies([FromQuery] UserDtoParameters parameters)
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
            var users = await _manageUser.GetCompaniesAsync(parameters);

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
            Console.Write(users.Count());
            return Ok(new { code=0,data=new{ items=users,total = users.TotalCount} });
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
