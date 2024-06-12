namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Bạn chưa nhập tên danh mục")]
        [StringLength(10,ErrorMessage ="Tên quá dài")]
        [DisplayName("Tên danh mục:")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Tiêu đề SEO:")]
        public string Alias { get; set; }
        [DisplayName("Danh mục cha:")]
        public int? ParentID { get; set; }
        [DisplayName("Ngày tạo:")]
        public DateTime Created_Date { get; set; }
        [DisplayName("Thứ tự:")]
        public int Order { get; set; }
        [DisplayName("Trạng thái:")]
        public bool Status { get; set; }
    }
}
