using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myshop.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace myshop.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Nonlogin()
        {
            return View();
        }
        public ActionResult Success()
        {
            var currentcart = Models.Cart.Operation.GetCurrentCart();  //取得目前購物車資料
            currentcart.Removeall(); //購物成功(即訂單已建立)，後清空購物車，重新購物
            return View();
        }
        // GET: Order
        public ActionResult Index()
        {
            var currentcart = myshop.Models.Cart.Operation.GetCurrentCart();//取得目前購物車狀態
            if (currentcart.Count() == 0) TempData["nonproduct"] = "購物車內無商品";
            ViewBag.nonproduct = TempData["nonproduct"];
            return View();
        }
        [HttpPost]
        public ActionResult Index(Order od)
        {
            CartsEntities db = new CartsEntities();
            if(this.ModelState.IsValid) //判斷目前Model狀態為有效值
            {
                var currentcart = myshop.Models.Cart.Operation.GetCurrentCart();//取得目前購物車狀態
                if (currentcart.Count() == 0) return RedirectToAction("Index");
                var userid = HttpContext.User.Identity.GetUserId();  //取得目前登入者ID
                if (!User.Identity.IsAuthenticated) //判斷是否登錄會員，如沒有
                {
                    return RedirectToAction("Nonlogin");  
                }
                var order = new Myorder()
                {
                    UserId = userid,
                    RecieverName = od.收貨人姓名,
                    RecieverPhone = od.收貨人電話,
                    RecieverAddress = od.收貨人地址
                };  //建立order實體物件，將userid、od值指向order
                db.Myorders.Add(order); //將order物件加入資料庫
                db.SaveChanges(); //儲存變更資料庫

                var orderdetail = currentcart.myorderdetailtolist(order.Id);  //建立orderdetail實體物件，呼叫myorderdetailtolist方法(傳入訂單編號)
                db.MyorderDetails.AddRange(orderdetail);   //將orderdetail物件加入資料庫
                db.SaveChanges();  //儲存變更資料庫
                return RedirectToAction("Success");   //回傳顯示"Success"
            }
            return View();
        }
        public ActionResult OrderproductDetail(int? id)
        {
            CartsEntities db = new CartsEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}