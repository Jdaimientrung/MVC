using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShopOnline.Areas.Admin.Models
{
    public class UserModels
    {
        public long UserID { get; set; }

        //[Required(ErrorMessage = "* Nhập user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "* Mật khẩu không được bỏ trống")]
        [StringLength(50)]
        public string Password { get; set; }
        [DisplayName("FullName")]
        [Required(ErrorMessage = "* Nhập họ tên")]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool Status { get; set; }
    }
}