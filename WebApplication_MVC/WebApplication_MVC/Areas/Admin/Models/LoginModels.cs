using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_MVC.Areas.Admin.Models
{
    public class LoginModels
    {
        [Required]//bắt buộc
        public string UserName { get; set;}
        public string Password { get; set;}
        public bool RememberMe { get; set;}
    }
}