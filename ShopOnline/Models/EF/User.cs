namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {

        public long UserID { get; set; }

        [Required(ErrorMessage = "* Nhập user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "* Mật khẩu không được bỏ trống")]
        [StringLength(50, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự..", MinimumLength = 8)]
       // [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}", ErrorMessage = " Mật khẩu bao gồm ít nhất một chữ thường, một chữ hoa, một số và một ký tự đặc biệt.")]
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

