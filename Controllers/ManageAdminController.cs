using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAdminController : Controller
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            if (GetRolesFromAuthorizZation() < 3)
            {
                return Json(new { code = -1, msg = "您没有权限进行用户管理！" });
            }
            //if (await AccountSystem.AddAdmin(admin))
                return Json(new { code = 0, msg = "成功添加新管理员！" });
            return Json(new { code = -1, msg = "添加管理员失败，请检查您输入的信息!" });
        }

        private int GetRolesFromAuthorizZation()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst("Roles")?.Value);
        }
    }
}
