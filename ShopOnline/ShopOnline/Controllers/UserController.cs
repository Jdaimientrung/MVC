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
using Facebook;
using System.Configuration;


namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new User();
                user.Email = email;
                user.UserName = email;
                user.Status = true;
                user.Name = firstname + " " + middlename + " " + lastname;

                var resultInsert = new UserDao().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                }
            }
            return Redirect("/");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;

            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else

                    if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else

                    if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else

                    if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                }
            }
            return View(model);

        }
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
                ModelState.AddModelError("Captcha", "Mã CAPTCHA không chính xác.");
            }
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var user = dao.GetUserByName(model.UserName);
                if (user == null)
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
                    model.Password = encryptedMd5Pas;
                    model.Status = true;
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