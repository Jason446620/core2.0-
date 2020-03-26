using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace coreEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginModule
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string PassWord { get; set; }
    }
}
