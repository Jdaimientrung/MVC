using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication_MVC.Models;

namespace WebApplication_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()//
        {
            //var message = new MessageModels();
            //message.Wellcome = "Chào mừng MessageModels";
            //ViewBag.WellcomeString = "Chào mừng đến với ViewBag";
            return View(/*message*/);
        }

        
    }
}