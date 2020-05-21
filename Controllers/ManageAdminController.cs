using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdministrationServiceBackEnd.DtoParameters;
using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAdminController : Controller
    {
        [Authorize]
        [HttpGet("GetAdmins")]
        [HttpHead]
        public async Task<IActionResult> GetAdmins([FromQuery] AdminDtoParameters parameters)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            var admins = await AdminSystem.GetAdminsAsync(parameters);
            return Ok(new { code = 0, data = new { items = admins, total = admins.TotalCount } });
        }
        [Authorize]
        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin(Admin admin)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            if (AdminSystem.AddAdmin(admin))
            {
                AdminSystem.AddLog(GetAdminName(), "添加新管理员："+admin.Username);
                return Json(new { code = 0, msg = "成功添加新管理员！" });
            }
            return Json(new { code = -1, msg = "添加管理员失败，请检查您输入的信息!" });
        }
        //Post api/<controller>/Changepwd
        [Authorize]
        [HttpPost("Changepwd")]
        public IActionResult Changepwd([FromBody]Admin admin)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            if (AdminSystem.ChangePassword(admin))
            {
                AdminSystem.AddLog(GetAdminName(),"修改管理员"+admin.Username+"的密码");
                return Json(new { code = 0, msg = "密码修改成功" });
            }
            return Json(new { code = -1, msg = "密码修改失败" });
        }
        //Post api/<controller>/ChangeName
        [Authorize]
        [HttpPost("ChangeName")]
        public IActionResult ChangeName([FromBody]Admin admin)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            if (AdminSystem.ChangeName(admin))
            {
                AdminSystem.AddLog(GetAdminName(), "修改用户名");
                return Json(new { code = 0, msg = "用户名修改成功" });
            }
            return Json(new { code = -1, msg = "用户名修改失败" });
        }
        // GET api/<controller>/Delete
        [Authorize]
        [HttpGet("Delete")]
        public IActionResult Delete([FromQuery]Admin admin)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            admin = AdminSystem.GetAdminById(admin);
            if (AdminSystem.DeleteAdmin(admin))
            {
                AdminSystem.AddLog(GetAdminName(), "删除用户："+admin.Username);
                return Json(new { code = 0, msg = "删除成功！" });
            }
            return Json(new { code = 0, msg = "删除失败！" });
        }
        [Authorize]
        [HttpGet("GetAdminLogs")]
        public IActionResult GetAdminLogs([FromQuery]Admin admin)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            var logs = Backup.GetAllLogs();
            return Json(new { code = 0, data=new { items = logs, total = logs.Count() } });
        }
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
