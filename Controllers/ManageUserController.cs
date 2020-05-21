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
        [Authorize]
        [HttpGet("GetUsers")]
        [HttpHead]
        public async Task<IActionResult> GetUser([FromQuery] UserDtoParameters parameters)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            MuseumContext _context = new MuseumContext();
            IManageUser _manageUser = new ManageUser(_context);
            var users = await _manageUser.GetUsersAsync(parameters);
            return Ok(new { code=0,data=new{ items=users,total = users.TotalCount} });
        }
        // POST api/<controller>/Changepwd
        [Authorize]
        [HttpPost("Changepwd")]
        public  IActionResult Changepwd([FromBody]User user)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限修改用户密码！" });
            if (UserSystem.ChangePassword(user))
            {
                AdminSystem.AddLog(GetAdminName(), "修改用户 " + user.Userid + " 的密码");
                return Json(new { code = 0, msg = "密码修改成功" });
            }
            return Json(new { code = -1, msg = "密码修改失败" });
        }
        // DELETE api/<controller>/5
        [Authorize]
        [HttpGet("Delete")]
        public IActionResult Delete([FromQuery]User user)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            if (UserSystem.DeleteUser(user))
            {
                AdminSystem.AddLog(GetAdminName(), "删除用户：" + user.Userid);
                return Json(new { code = 0, msg = "删除成功！" });
            }
            return Json(new { code = -1, msg = "删除失败！" });
        }
        [Authorize]
        [HttpGet("ChangeMute")]
        public IActionResult ChangeMute([FromQuery]User user)
        {
            if (!JudgeRoles(1))
                return Json(new { code = -1, msg = "您没有权限进行此操作！" });
            if (UserSystem.ChangeMute(user))
            {
                if(user.Coright == 0)
                    AdminSystem.AddLog(GetAdminName(), "禁言用户：" + user.Userid);
                else
                    AdminSystem.AddLog(GetAdminName(), "解禁用户：" + user.Userid);
                return Json(new { code = 0, msg = "修改禁言状态成功！" });
            }
            return Json(new { code = -1, msg = "更改失败！" });
        }
        private bool JudgeRoles(int x)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            return int.Parse(claimsIdentity.FindFirst("Roles")?.Value)>=x;
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
