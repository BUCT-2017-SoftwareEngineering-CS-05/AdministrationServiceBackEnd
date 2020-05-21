﻿using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuseumController : Controller
    {
        // GET: api/<controller>
        [Authorize]
        [HttpGet("GetMuseums")]
        public async Task<IActionResult> GetMuseums()
        {
            var maintables= await MuseumSystem.GetMaintable();
            return Json(new { code=0,data = new { items = maintables, total = maintables.Count() } });
        }
        [Authorize]
        [HttpGet("GetMuseumByMidex")]
        public IActionResult GetMuseum([FromQuery]Maintable maintable)
        {
            var maintable1 = MuseumSystem.GetMuseumByMidex(maintable.Midex);
            return Json(new { code = 0, data = maintable1 });
        }
        [Authorize]
        [HttpGet("GetExhibitionByMidex")]
        public IActionResult GetExhibitionByMidex([FromQuery]Maintable maintable)
        {
            var exhibitions = MuseumSystem.GetExhibitionByMidex(maintable.Midex);
            return Json(new { code = 0, data = new { items = exhibitions, total = exhibitions.Count() } });
        }
        [Authorize]
        [HttpGet("GetEducationByMidex")]
        public IActionResult GetEducationByMidex([FromQuery]Maintable maintable)
        {
            var educations = MuseumSystem.GetEducationByMidex(maintable.Midex);
            return Json(new { code = 0, data = new { items = educations, total = educations.Count() } });
        }
        [Authorize]
        [HttpGet("GetCommentByMidex")]
        public async Task<IActionResult> GetCommentByMidex([FromQuery]Maintable maintable)
        {
            var comment =  await MuseumSystem.GetCommentByMidex(maintable.Midex);
            return Json(new { code = 0, data = new { items = comment, total = comment.Count() } });
        }
        [Authorize]
        [HttpGet("GetNewsByMidex")]
        public async Task<IActionResult> GetNewsByMidex([FromQuery]CollectionDtoParameters parameters)
        {
            var news = await MuseumSystem.GetNewsByMidex(parameters);
            return Json(new { code = 0, data = new { items = news, total = news.TotalCount } });
        }
        [Authorize]
        [HttpGet("GetCollectionsByMidex")]
        public async Task<IActionResult> GetCollectionsByMidex([FromQuery]CollectionDtoParameters parameters)
        {
            var collections = await MuseumSystem.GetCollectionsAsync(parameters);
            return Json(new { code = 0, data = new { items = collections, total = collections.TotalCount } });
        }
        [Authorize]
        [HttpPost("DeleteMuseumByMidex")]
        public IActionResult DeleteMuseumByMidex([FromBody]Maintable maintable)
        {
            if( MuseumSystem.DeleteMuseumByMidex(maintable))
                return Json(new { code = 0, msg = "删除成功" });
            return Json(new { code = -1, msg = "删除失败" });
        }



        //[Authorize]
        //[HttpGet("DeleteExhibitionByEid")]
        //public IActionResult DeleteExhibitionByeid([FromQuery]Exhibition exhibition)
        //{
        //    if(MuseumSystem.DeleteExhibitionByEid(exhibition.Eid))
        //        return Json(new { code = 0, msg = "删除成功" });
        //    return Json(new { code = -1, msg = "删除失败" });
        //}
        //[Authorize]
        //[HttpGet("DeleteEducationByAid")]
        //public IActionResult DeleteEducationByAid([FromQuery]Education education)
        //{
        //    if(MuseumSystem.DeleteEducationByAid(education))
        //        return Json(new { code = 0, msg = "删除成功" });
        //    return Json(new { code = -1, msg = "删除失败" });
        //}
        [Authorize]
        [HttpGet("DeleteCommentByUseridMidex")]
        public IActionResult DeleteCommentByUseridMidex([FromQuery]Comment comment)
        {
            if (UserSystem.DeleteOneComment(comment))
                return Json(new { code = 0, msg = "删除成功" });
            return Json(new { code = -1, msg = "删除失败" });
        }
        //[Authorize]
        //[HttpGet("DeleteNewsById")]
        //public IActionResult DeleteNewsById([FromQuery]News news)
        //{
        //    if(MuseumSystem.DeleteNewsById(news))
        //        return Json(new { code = 0, msg = "删除成功" });
        //    return Json(new { code = -1, msg = "删除失败" });
        //}
        //[Authorize]
        //[HttpGet("DeleteCollectionsByOid")]
        //public IActionResult DeleteCollectionsByOid([FromQuery]Collection collection)
        //{
        //    if( MuseumSystem.DeleteCollectionsByOid(collection))
        //        return Json(new { code = 0, msg = "删除成功" });
        //    return Json(new { code = -1, msg = "删除失败" });
        //}
        private bool JudgeRoles(int x)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst("Roles")?.Value) >= x;
        }
        private string GetAdminName()
        {
            var admin = new Admin();
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            admin.Username = claimsIdentity.FindFirst("Username")?.Value;
            admin.Roles = int.Parse(claimsIdentity.FindFirst("Roles")?.Value);
            admin.Id = int.Parse(claimsIdentity.FindFirst("Id")?.Value);
            return admin.Username;
        }
    }
}
