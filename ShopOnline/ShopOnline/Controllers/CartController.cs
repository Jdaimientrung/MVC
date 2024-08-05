using Models.Dao;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Models.EF;
using System.Configuration;
using System.Reflection;
using Common;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: CartItem
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list=new List<CartItemModels>();
            if (cart != null)
            {
                list = (List<CartItemModels>)cart;
            }

            return View(list);
        }
        [HttpPost]
        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                
                var list = (List<CartItemModels>)cart;
                if (list.Exists(x => x.Product.ProductID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ProductID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng
                    var item = new CartItemModels();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //gán vào session
                Session[CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng
                var item = new CartItemModels();

                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItemModels>();
                list.Add(item);
                //gán vào session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public JsonResult Update(string cartModel) 
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItemModels>>(cartModel);
            var sessionCart= (List<CartItemModels>)Session[CartSession];
            foreach (var item in sessionCart) 
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ProductID == item.Product.ProductID);
          //      var jsonItem=jsonCart.SingleOrDefault(x=>x.Product.ProductID==item.Product.ProductID);
                if(jsonItem != null)
                {
                    item.Quantity=jsonItem.Quantity;
                    
                }
            }
            Session[CartSession] = sessionCart;
            
            return Json(new
            {
                status = true
            });
               
        }
        public JsonResult DeleteAll() 
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItemModels>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ProductID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItemModels>();
            if (cart != null)
            {
                list = (List<CartItemModels>)cart;
            }

            return View(list);
           
        }
        [HttpPost]
        public ActionResult Payment(string shipName,string address, string email, string phone)
        {
            var order= new Order();
            order.OrderDate = DateTime.Now;
            order.ShipName=shipName;
            order.ShipAddress=address;
            order.Email=email;
            order.PhoneNumber=phone;
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItemModels>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = id;
                    orderDetail.ProductID = item.Product.ProductID;
                    orderDetail.UnitPrice = item.Product.UnitPrice;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.UnitPrice.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception ex) 
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
                throw;
            } 
            return Redirect("/Cart/Success");

        }
        public ActionResult Success()
        {
            return View();
        }
    }
}