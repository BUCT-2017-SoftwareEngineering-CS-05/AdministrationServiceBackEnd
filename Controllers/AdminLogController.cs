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
            if (AccountSystem.LogIn(admin.Username, admin.Password))
            {
                return Json(new { code = 0, data = new{ name = admin.Username,pwd = admin.Password,token = GetToken(admin) },msg="登录成功" });
            }
            return Json(new { code= -1, msg = "用户名或密码错误" });
            return Json(new { data = "Fail" });
            
        }
        [Authorize]
        [HttpGet("Info")]
        public IActionResult Info()
        {
            return Json(new { code = 0, data = new { roles = "admin0", id = 1, username = "admin", avatar = "https://www.youbaobao.xyz/mpvue-res/logo.jpg" } });
        }
        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Json(new { code = 0, data = "success" });
        }
        [Authorize]
        [HttpGet("name")]
        public IActionResult name()
        {
            //var headers = _httpContextAccessor.HttpContext.Request.Headers;

            //var admin=headers["Authorization"];
            //var Claims = admin.Claims.Count(); //正常是可以获取到所有的Claims的，我试验如果吧token的串修改的话这个地方就取不到了，但还是会进来这个方法，所以要判断下是不是null
            //return Json(Claims);
            //var sub = User.FindFirst(d => d.Type == JwtRegisteredClaimNames.Sub)?.Value;
            //if (string.IsNullOrEmpty(sub))
            //{
            //    return Json(new { code = "0", id = "", strErr = "Token验证失败" });
            //}
            //var result = _Ziphelper.CreateFileAndZip(id);
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Username")?.Value;
            return Json(new{name = userId });
            //return Json(new { code = result.Code, Id = result.Id, strErr = result.strErr });
        }
        private string GetToken(Admin admin)
        {
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim("Username", admin.Username),
                new Claim(JwtRegisteredClaimNames.Sub, admin.Username),
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