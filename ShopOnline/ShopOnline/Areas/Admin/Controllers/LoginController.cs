﻿using Models.Dao;
using ShopOnline.Areas.Admin.Models;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password),true);
                if (result==1)
                {
                    var user=dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID=user.UserID; 
                    Session.Add(CommonConstants.USER_SESSION,userSession);
                    return RedirectToAction("Index","Home");
                }
                else
                
                    if(result==0) 
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

                    if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                }
            }
            return View("Index");
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}