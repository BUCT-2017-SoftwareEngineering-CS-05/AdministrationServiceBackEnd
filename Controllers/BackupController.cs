using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class BackupController : Controller
    {
        // GET: api/<controller>
        [HttpGet("Backup")]
        public IActionResult Backups()
        {
            if (Backup.Save())
            {
                AdminSystem.AddLog(GetAdminName(), "备份当前数据库");
                return Json(new { code = 0, msg = "备份成功！" });
            }
            return Json(new { code = -1, msg = "备份失败！" });
        }
        [HttpGet("Restore")]
        public IActionResult Restore([FromQuery]string fname)
        {
            if (Backup.Load(fname))
            {
                AdminSystem.AddLog(GetAdminName(), "恢复数据库为当前状态");
                return Json(new { code = 0, msg = "恢复成功！" });
            }
            return Json(new { code = -1, msg = "恢复失败！" });
        }
        [HttpGet("GetAllBackups")]
        public IActionResult GetAllBackups()
        {
            var backups=Backup.GetAllBackups();
            return Json(new {data = new{items=backups,total=backups.Count()} });
        }
        [HttpGet("DeleteBackup")]
        public IActionResult DeleteBackupByFname([FromQuery]string fname)
        {
            if (Backup.DeleteBackupByFname(fname))
            {
                AdminSystem.AddLog(GetAdminName(), "删除时间为"+fname+"的备份");
                return Json(new { code = 0, msg = "删除成功！" });
            }
            return Json(new { code = -1, msg = "删除失败！" });
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
