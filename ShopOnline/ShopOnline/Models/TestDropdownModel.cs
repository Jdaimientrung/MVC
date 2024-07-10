using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class TestDropdownModel
    {
        [Key]
        public long UserID { get; set; }

        //[Required(ErrorMessage = "* Nhập user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "* Mật khẩu không được bỏ trống")]
        [StringLength(50)]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
     
        [StringLength(250)]
        public string Name { get; set; }
        [Display(Name ="Tỉnh/Thành")]
        [StringLength(50)]

        public string ProvinceID { get; set; }
        [Display(Name = "Quận/Huyện")] 
        public string DistrictID { get; set; }
      
        [Display(Name = "Xã/Phường")]
        public string PrecinctID { get; set; }
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public bool Status { get; set; }
    }
}