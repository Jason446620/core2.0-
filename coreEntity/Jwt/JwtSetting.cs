using System;
using System.Collections.Generic;
using System.Text;

namespace coreEntity
{
    /// <summary>
    /// Jwt
    /// </summary>
    public class JwtSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SecretKey { get; set; }
    }
}
