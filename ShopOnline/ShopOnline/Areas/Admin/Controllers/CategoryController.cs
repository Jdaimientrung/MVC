using Models.Dao;
using Models.EF;
using PagedList;
using ShopOnline.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private ShopOnlineDbContext db=new ShopOnlineDbContext();

        // GET: Admin/Category
        public ActionResult Index(string searchString, int Page = 1, int PageSize = 5)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, Page, PageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryModels model/*, HttpPostedFileBase imageFile*/)
        {
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension=Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            model.ImagePath= "~/Assets/admin/assets/img/"+fileName;
            fileName = Path.Combine(Server.MapPath("~/Assets/admin/assets/img/"), fileName);
            model.ImageFile.SaveAs(fileName);
           // if(ModelState.IsValid)
           //{
           //if (imageFile != null)
           //{
           //    category.Picture = new byte[imageFile.ContentLength];
           //    imageFile.InputStream.Read(category.Picture, 0, imageFile.ContentLength);
           //}
                var abc= new Category();
            abc.CategoryName = model.CategoryName;
            abc.Description = model.Description;
            abc.Picture=model.ImagePath;

                db.Categories.Add(abc);
                db.SaveChanges();
                return RedirectToAction("Index");
          //  }
            return View(model);
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }

    }
}