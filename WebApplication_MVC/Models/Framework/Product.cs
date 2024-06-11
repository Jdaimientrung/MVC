namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Alias { get; set; }

        public int? CategoryID { get; set; }

        [Required]
        [StringLength(250)]
        public string Images { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal Price { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Detail { get; set; }

        public bool Status { get; set; }
    }
}
