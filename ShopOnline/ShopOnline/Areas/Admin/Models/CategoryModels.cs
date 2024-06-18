using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Areas.Admin.Models
{
    public partial class CategoryModels
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }  

    }
}