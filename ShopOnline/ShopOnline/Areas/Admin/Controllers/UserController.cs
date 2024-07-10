using Models.Dao;
using Models.EF;
using ShopOnline.Areas.Admin.Models;
using ShopOnline.Common;
using ShopOnline.Models;
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
        
        [HttpGet]      
        public ActionResult Edit(int id) 
        {
            var dao = new UserDao();
            var user = dao.GetUserByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userModel = new UserModels
            {
                UserID = user.UserID,
                UserName = user.UserName,
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                Phone = user.Phone,
                Status = user.Status
            };

            return View(userModel);
        }

        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var user = dao.GetUserByName(model.UserName);
                if (user == null)
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(model.Password);
                    model.Password = encryptedMd5Pas;
                    var newUser = new User();
                    newUser.UserName = model.UserName;
                    newUser.Password = encryptedMd5Pas;
                    newUser.Email = model.Email;
                    newUser.Phone = model.Phone;
                    newUser.Status = model.Status;
                    newUser.Address = model.Address;
                    dao.Insert(newUser);
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index");

                }
                else
                {
                    SetAlert("Thêm user không thành công", "error");
                    ModelState.AddModelError("", "User tồn tại");

                }
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Edit(UserModels model, string currentPassword)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var user = dao.GetUserByID(model.UserID);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(currentPassword))
                    {
                        var encryptedCurrentPassword = Encryptor.MD5Hash(currentPassword);
                        if (user.Password == encryptedCurrentPassword)
                        {
                            user.Name = model.Name;
                            user.Address = model.Address;
                            user.Email = model.Email;
                            user.Phone = model.Phone;
                            user.Status = model.Status;

                            if (!string.IsNullOrEmpty(model.Password))
                            {
                                var encryptedNewPassword = Encryptor.MD5Hash(model.Password);
                                user.Password = encryptedNewPassword;
                            }

                            dao.Update(user);
                            SetAlert("Cập nhật thông tin thành công", "success");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Vui lòng nhập mật khẩu hiện tại để cập nhật thông tin.");
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(model);
        }
      
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            var result = dao.Delete(id);
            if (result)
            {
                SetAlert("Xóa user thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xóa không thành công.");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var dao = new UserDao();
            var result = dao.ChangeStatus(id);

          //  return Json(new { message = (result == false ? "Khóa thành công" : " kích hoạt thành công"), type = "success" }, JsonRequestBehavior.AllowGet);
            return Json(new
            {
                message = result ? "kích hoạt thành công" : "Khóa thành công",
                type = "success",
                newStatus = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}