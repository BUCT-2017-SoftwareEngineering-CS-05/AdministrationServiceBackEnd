using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using AdministrationServiceBackEnd.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuseumController : Controller
    {
        // GET: api/<controller>
        [HttpGet("GetMuseums")]
        public IActionResult GetMuseums()
        {
            var maintables=MuseumSystem.GetAllMuseums();
            int tol=maintables.Count();
            return Json(new { data= JsonConvert.SerializeObject(maintables), total =tol});
        }
    }
}
