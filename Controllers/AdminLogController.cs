using AdministrationServiceBackEnd.Models;
using AdministrationServiceBackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdministrationServiceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLogController : Controller
    {
        // GET: /AdminLog/
        private readonly JwtConfig jwtModel = null;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminLogController(IOptions<JwtConfig> _jwtModel)//注入jwt配置参数
        {
            jwtModel = _jwtModel.Value;
            //_httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Admin admin)
        {
            MuseumContext _context = new MuseumContext();
            IManageAdmin _manage = new ManageAdmin(_context);
            if (_manage.GetAdminByUsername(admin.Username) == null)
                return Json(new { code = -1, msg = "用户名不存在" });
            if (_manage.CheckPassword(admin.Username, admin.Password))
            {
                admin = _manage.GetAdminByUsername(admin.Username);
                return Json(new { code = 0, data = new { token = GetToken(admin) }, msg = "登录成功" });
            }           
            return Json(new { code = -1, msg = "用户名或密码错误" });
        }
        [Authorize]
        [HttpGet("Info")]
        public IActionResult Info()
        {
            Admin admin = GetAdminFromAuthorizZation();
            List<string> roles = new List<string>();
            roles.Add(admin.Roles == 3 ? "admin" : "editor");
            return Json(new { code = 0, 
                data = new {
                    roles = roles,
                    id = admin.Id, 
                    username = admin.Username,
                    avatar = "https://www.youbaobao.xyz/mpvue-res/logo.jpg" }
            });
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Json(new { code = 0, data = "退出成功" });
        }
        private Admin GetAdminFromAuthorizZation()
        {
            var admin = new Admin();
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            admin.Username = claimsIdentity.FindFirst("Username")?.Value;
            admin.Roles = int.Parse(claimsIdentity.FindFirst("Roles")?.Value);
            admin.Id = int.Parse(claimsIdentity.FindFirst("Id")?.Value);
            return admin;
        }
        private string GetToken(Admin admin)
        {
            
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim("Username", admin.Username),
                new Claim("Roles", admin.Roles.ToString()),
                new Claim("Id", admin.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                });
            DateTime now = DateTime.UtcNow;
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtModel.Issuer,
                audience: jwtModel.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(jwtModel.Expiration)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtModel.SecurityKey)), SecurityAlgorithms.HmacSha256)
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}