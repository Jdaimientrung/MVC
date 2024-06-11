using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication_MVC.Areas.Admin.Code;
using WebApplication_MVC.Areas.Admin.Models;

namespace WebApplication_MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]//nhận từ url
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]// không thể nhận từ url
        [ValidateAntiForgeryToken]//
        public ActionResult Index(LoginModels model)
        {
            //var result = new AccountModel().Login(model.UserName, model.Password);
            if (/*result*/Membership.ValidateUser(model.UserName,model.Password) && ModelState.IsValid)
            {
                //SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}