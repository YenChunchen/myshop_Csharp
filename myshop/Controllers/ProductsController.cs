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
    public class ProductsController : Controller
    {
        private CartsEntities db = new CartsEntities();

        // GET: Products
        public ActionResult Index(string search,string Gren)
        {
            ViewBag.create = TempData["create"];
            ViewBag.editnotfound = TempData["editnotfound"];
            var grelist = new List<string>();  //建立一個空的list(用於儲存商品類別)
            var greall = from g in db.Products  //從db中撈取資料(商品類別)，並依商品類別排序
                         orderby g.商品類別
                         select g.商品類別;
            grelist.AddRange(greall.Distinct());  //將資料取唯一值(Distinct())加入建立的list物件中
            ViewBag.Gren = new SelectList(grelist);
            //將該list包裝(SelectList()選擇欲加入的清單)進viewbag中(才能在View中顯示)
            var pro = from m in db.Products  //建立movie物件=//從db中撈取所有資料
                        select m;
            if (!string.IsNullOrEmpty(search))  //輸入的字串是否存在
            {
                pro = pro.Where(s => s.商品名稱.Contains(search));  //pro=商品名稱中符合輸入的相關字的資料
            }
            if (!string.IsNullOrEmpty(Gren))  //判斷輸入類別是否存在
            {
                pro = pro.Where(x => x.商品類別== Gren);  //pro=商品類別中選取該類別的資料
            }
            return View(pro);  //顯示結果
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
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

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "商品編號,商品名稱,商品類別,類別編號,製造日期,商品描述,價格,是否庫存,商品圖")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                TempData["create"] = "商品["+product.商品名稱.ToString() + "]已成功建立!";
                return RedirectToAction("Index");
            }
            ViewBag.createfail = "商品建立失敗!";
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)  //如URL未輸入id值
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  
            }
            Product product = db.Products.Find(id);  //從資料庫中查詢是否有該id項目
            if (product == null)  //找不到該項目
            {
                TempData["editnotfound"] = "找不到該項商品!"; //建立一暫存資料，提醒錯誤訊息
                return RedirectToAction("Index");  //重導向到Index
            }
            return View(product);  //找到該項目即將其值傳至Edit.cshtml
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "商品編號,商品名稱,商品類別,類別編號,製造日期,商品描述,價格,是否庫存,商品圖")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
