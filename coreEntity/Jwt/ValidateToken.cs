using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace coreEntity.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public class myValidateToken : ISecurityTokenValidator
    {
        /// <summary>
        /// 
        /// </summary>
        public bool CanValidateToken => true;
        /// <summary>
        /// 
        /// </summary>
        public int MaximumTokenSizeInBytes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        public bool CanReadToken(string securityToken)
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="validationParameters"></param>
        /// <param name="validatedToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            ////判断token是否正确
            //if (securityToken != "myTokenSecret")
            //    return null;

            //给Identity赋值
            //var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim("name", "jackyfei"));
            //identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));

            //var principle = new ClaimsPrincipal(identity);
            //return principle;

            // validatedToken = null;
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            if (securityToken == "myTokenSecret")
            {
                identity.AddClaim(new Claim("name", "jackyfei"));
                identity.AddClaim(new Claim("admin", "true"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType,"admin"));
            }

            var principal = new ClaimsPrincipal(identity);

            return principal;

        }
    }
}