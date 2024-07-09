using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class RegisterModel
    {
        public long UserID { get; set; }

        //[Required(ErrorMessage = "* Nhập user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "* Mật khẩu không được bỏ trống")]
        [StringLength(50)]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }
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
    //public class RegisterModel
    //{

    //    public long UserID { set; get; }

    //    [Display(Name = "Tên đăng nhập")]
    //  //  [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]

    //    public string UserName { set; get; }

    //    [Display(Name = "Mật khẩu")]
    //  //  [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
    // //   [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
    //    public string Password { set; get; }

    //    [Display(Name = "Xác nhận mật khẩu")]
    //    [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
    //    public string ConfirmPassword { set; get; }

    //    [Display(Name = "Họ tên")]
    // //   [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
    //    public string Name { set; get; }

    //    [Display(Name = "Địa chỉ")]
    //    public string Address { set; get; }

    // //   [Required(ErrorMessage = "Yêu cầu nhập email")]
    //    [Display(Name = "Email")]
    //    public string Email { set; get; }

    //    [Display(Name = "Điện thoại")]
    //    public string Phone { set; get; }
    //    public bool Status { get; set; }



    //}
}



