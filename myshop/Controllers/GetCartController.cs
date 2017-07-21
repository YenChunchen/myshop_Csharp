using myshop.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myshop.Controllers
{
    public class GetCartController : Controller
    {
        public ActionResult GetCart()
        {
            var currentcart = Models.Cart.Operation.GetCurrentCart(); //取得目前購物車內容
            currentcart.AddCartItem(1);
            return Content("目前購物車累計"+currentcart.totalamount.ToString()+"元");
        }
    }
}