using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myshop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult GetCart()
        {
            return PartialView("_CartPartView");
        }
        public ActionResult Buyit(int id)  //加入購物車Action
        {
            var currentcart = Models.Cart.Operation.GetCurrentCart();  //取得目前購物車資料
            currentcart.AddCartItem(id);  
            return PartialView("_CartPartView");
        }
        public ActionResult Removeit(int id)  //從購物車移除該品項Action
        {
            var currentcart = Models.Cart.Operation.GetCurrentCart();  //取得目前購物車資料
            currentcart.Removeitem(id);
            return PartialView("_CartPartView");
        }
        public ActionResult Removeall()  //將購物車清空Action
        {
            var currentcart = Models.Cart.Operation.GetCurrentCart();  //取得目前購物車資料
            currentcart.Removeall();
            return PartialView("_CartPartView");
        }
    }
}