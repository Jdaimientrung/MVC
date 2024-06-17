using Models.Dao;
using Models.EF;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User

        public ActionResult Index(string searchString, int Page=1,int PageSize=5)
        {
            var dao=new UserDao();
            var model=dao.ListAllPaging(searchString, Page, PageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDeltail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var user = dao.GetUserByName(model.UserName);
                if (user == null)
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
                    model.Password = encryptedMd5Pas;
                    dao.Insert(model);
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "User tồn tại");

                }
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Vui lòng nhập mật khẩu để cập nhật.");
                    return View(model);
                }
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
                    model.Password = encryptedMd5Pas;
                }
                
               

                var result= dao.Update(model);
                if (result)
                {
                    return RedirectToAction("Index","User");
                }
             
                else
                {
                    ModelState.AddModelError("", "Cập nhật thành công");

                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            var result = dao.Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa không thành công.");
            }
            return RedirectToAction("Index");
        }
    }
}