using Microsoft.AspNet.Identity;
using myshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace myshop.Controllers
{
    public class HomeController : Controller
    {
        private CartsEntities db = new CartsEntities();  //連結至資料庫
        public ActionResult Index()
        {
            var list = (from pro in db.Products select pro).ToList();   //連結至資料庫
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductDetail(int? id)
        {
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

        [HttpPost]  //HTTP REQUEST為POST狀態時呼叫
        [Authorize]  //必須登入
        public ActionResult AddProductComment(int id, string comment)  //引入值為ProductDetail.cshtml回傳值
        {
            CartsEntities db = new CartsEntities();  //建立資料庫CartsEntities實體db
            UserEntities db2 = new UserEntities();   //建立資料庫UserEntities實體db2
            var userid = HttpContext.User.Identity.GetUserId();  //宣告變數userid=目前登入者的Id
            var username = (from u in db2.AspNetUsers   //宣告變數username=從AspNetUsers資料表中
                            where u.Id == userid    //選取Id=userid
                            select u.UserName).FirstOrDefault();  //取得該UserName欄位
            var currenttime = DateTime.Now;  //宣告變數currenttime=目前時間
            var procomment = new ProductComment()  //建立ProductComment物件實體procomment
            {  //以下為該物件內容值
                會員名稱=username,
                留言產品編號 = id,
                會員編號 = userid,
                留言內容 = comment,
                留言時間 = currenttime
            };
            db.ProductComments.Add(procomment);  //將procomment加入資料表ProductComments中
            db.SaveChanges();  //更新資料庫
            return RedirectToAction("ProductDetail", new { id = id });  //指向Action ProductDetail(引入id值)
        }
    }
}