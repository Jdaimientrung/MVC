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

        [Required(ErrorMessage = "* Nh?p user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "* M?t kh?u không ???c b? tr?ng")]
        [StringLength(50)]
        public string Password { get; set; }
        [DisplayName("FullName")]
        [Required(ErrorMessage = "* Nh?p h? tên")]
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

