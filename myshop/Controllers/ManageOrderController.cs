using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myshop.Models;
using Microsoft.AspNet.Identity;

namespace myshop.Controllers
{
    public class ManageOrderController : Controller
    {
        private CartsEntities db = new CartsEntities();
        public ActionResult Index(string searchid)  //引入值searchid為Index.cshtml中textbox內容
        {
            var result = from r in db.Myorders //宣告物件result=Myorders資料表中內容
                         select r;
            if(!string.IsNullOrEmpty(searchid)) //判斷引入值是否為為null，如沒有
            {
                result = result.Where(r => r.Id.ToString().Contains(searchid));
                //result=從result中篩選Id欄位其內容有[searchid]引入值的資料
            }
            return View(result); //將結果送回View中
        }  
        // GET: Myorders/Details/5
        public ActionResult Details(int? id)
        {
            var currentcart = (from items in db.MyorderDetails  //從MyorderDetails中
                               where items.MyorderId == id  //選取MyorderId符合id
                               select items).ToList();  //的物件轉成List
            if (currentcart.Count == 0) return RedirectToAction("Index");  //判斷是否有該筆訂單
            else return View(currentcart);  //該訂單存在
        }

        // GET: Myorders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Myorder myorder = db.Myorders.Find(id);
            if (myorder == null)
            {
                return HttpNotFound();
            }
            return View(myorder);
        }

        // POST: Myorders/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RecieverName,RecieverPhone,RecieverAddress")] Myorder myorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myorder);
        }

        // GET: Myorders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Myorder myorder = db.Myorders.Find(id);
            if (myorder == null)
            {
                return HttpNotFound();
            }
            return View(myorder);
        }

        // POST: Myorders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)  
        {
            Myorder myorder = db.Myorders.Find(id);  //找到資料表Myorders中訂單編號符合該id(訂單編號)的資料
            var delorder = (from items in db.MyorderDetails //找到資料表MyorderDetails中訂單編號符合該id的資料
                            where items.MyorderId == id
                               select items).ToList();
            foreach(var i in delorder)  //刪除資料表MyorderDetails中訂單編號符合該id的資料
            {
                db.MyorderDetails.Remove(i);
                db.SaveChanges();
            }
            db.Myorders.Remove(myorder); //刪除資料表Myorders中訂單編號符合該id的資料
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult membercurrentorder()
        {
            var userid = HttpContext.User.Identity.GetUserId(); //取得目前登入者名稱(hash)
            UserEntities namedbd = new UserEntities();   
            string name = (from s in namedbd.AspNetUsers 
                          where s.Id == userid
                          select s.UserName).FirstOrDefault();
            ViewBag.Name = name;
            //-------以上為找尋該登入者名稱---------------------
            var result = (from r in db.Myorders
                         where r.UserId == userid
                         select r).ToList();
            return View(result);
            //-----以上為找尋該登入者所有訂單---------------------
        }

        public ActionResult MemberDetails(int? id)
        {
            var currentcart = (from items in db.MyorderDetails  //從MyorderDetails中
                               where items.MyorderId == id  //選取MyorderId符合id
                               select items).ToList();  //的物件轉成List
            if (currentcart.Count == 0) return RedirectToAction("Index");  //判斷是否有該筆訂單
            else return View(currentcart);  //該訂單存在
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
