using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using coreEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace core2._0_.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private JwtSetting _jwtSetting;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtSetting"></param>
        public AuthorizeController(IOptions<JwtSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }


        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Get(LoginModule login)
        {
            login.UserName = "jackyfei";
            login.PassWord = "123qwe";
            if (ModelState.IsValid)
            {
                if (!(login.UserName == "jackyfei" && login.PassWord == "123qwe"))
                {
                    return BadRequest();
                }
                var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,"jackyfei"),
                    //new Claim(ClaimTypes.Role,"admin"),
                    new Claim("Admin","true"),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _jwtSetting.Issuer,
                    _jwtSetting.Audience,
                    claims,
                    null,
                    DateTime.Now.AddMinutes(120),
                    credentials
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }
    }
}