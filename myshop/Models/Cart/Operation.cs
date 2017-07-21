using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace myshop.Models.Cart
{
    public class Operation //購物車的操作
    {
        [WebMethod(EnableSession =true)]  //開啟session，WebMethod(可由用戶端開啟)
        public static CartList GetCurrentCart()  
        //建立一靜態方法GetCurrentCart，回傳Cart型態物件(取得目前購物車內容
        {
            if(HttpContext.Current!=null)  //判斷HttpRequst是否為空
            {  //HttpRequst有值
                if (HttpContext.Current.Session["Cart"]==null)  //判斷HttpRequst的Session是否為空
                {  //如Session為空，即第一次購買
                    var order = new CartList();  //如為空，呼叫建構子，建立一個空的購買清單物件order
                    HttpContext.Current.Session["Cart"] = order;  //將order指派給Session["Cart"]
                }
                return (CartList)HttpContext.Current.Session["Cart"];  
                //回傳目前CartList型態的Session["Cart"]值
            }
            else
            {
                throw new InvalidOperationException("操作失敗，HttpRequst.Current為空，請檢查");
            }
        }
    }
}