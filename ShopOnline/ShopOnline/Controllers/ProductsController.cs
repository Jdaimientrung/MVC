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
      
        public ActionResult Index( int Page = 1, int PageSize = 6)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging( Page, PageSize);
            
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var product=new ProductDao().ViewDetail(id);
            return View(product);
        }
    }
}