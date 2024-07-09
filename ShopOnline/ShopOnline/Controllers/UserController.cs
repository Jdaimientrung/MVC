using BotDetect.Web.Mvc;
using Models.Dao;
using Models.EF;
using ShopOnline.Common;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using reCaptcha;
using Newtonsoft.Json.Linq;
using System.Net;


namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(User model)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = System.Configuration.ConfigurationManager.AppSettings["ReCaptcha.SecretKey"];
            var client = new WebClient();
            var result = client.DownloadString(
                string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            if (!status)
            {
                ModelState.AddModelError("Captcha", "Mã CAPTCHA không đúng.");
            }
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var user = dao.GetUserByName(model.UserName);
                if (user == null)
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
                    model.Password = encryptedMd5Pas;
                    dao.Insert(model);
                   // SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("/Register");

                }
                else
                {
                    //SetAlert("Thêm user không thành công", "error");
                    ModelState.AddModelError("", "User tồn tại");

                }
            }
            return View("Register");
        }
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new UserDao();
        //        if (dao.CheckUserName(model.UserName))
        //        {
        //            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");

        //        }
        //        else if (dao.CheckEmail(model.Email))
        //        {
        //            ModelState.AddModelError("", "Email đã tồn tại");
        //        }
        //        else
        //        {
        //            var user = new User();

        //            user.Name = model.Name;
        //            user.Password = model.Password;
        //            user.Phone = model.Phone;
        //            user.Email = model.Email;
        //            user.Address = model.Address;
        //            user.Status = true;
        //            var result = dao.Insert(user);
        //            if (result > 0)
        //            {
        //                ViewBag.Success = "Đăng kí thành công";
        //                model = new RegisterModel();

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Đăng kí không thành công");
        //            }

        //        }


        //    }
        //    return View(model);
        //}
    }
}