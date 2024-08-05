using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Product

        public ActionResult Index(string searchString,int Page = 1, int PageSize = 6)
        {
            var dao = new ProductDao();

            var model = dao.ListAllPaging(searchString, Page, PageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true,
            },JsonRequestBehavior.AllowGet);
        }
    }
}