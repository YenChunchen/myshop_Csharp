using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myshop.Models.Cart
{
    [Serializable]
    public class CartList :IEnumerable<CartItem> //購買清單
    {
        private List<CartItem> buylist;  //將購買商品存於buylist
        public CartList()  //宣告一建構子CartList，建立購買商品集合
        {    this.buylist = new List<CartItem>();   }

        public int totalcount  //計算所有商品數量
        {
            get
            {
                int total = 0;
                foreach (var c in buylist)
                {   total = total + c.購買數量;  }
                return total;
            }
        }

        public decimal totalamount  //計算所有商品總和
        {
            get
            {
                decimal total = 0;
                foreach(var c in buylist)
                { total = total + c.商品小計;   }
                return total;
            }
        }

        CartsEntities db = new CartsEntities();  //建立資料庫實體db
        public bool AddCartItem(int itemid)  //判斷該物品是否為初次購買
        {
            var finditem = this.buylist.  //從目前購物車清單buylist中
                Where(s => s.商品編號 == itemid) //找尋符合itemid的值
                .Select(s => s).FirstOrDefault();  //撈取符合結果
            if (finditem == default(CartItem))  //判斷是否在購物清單中，如沒有
            {
                var pro = (from s in db.Products //從資料庫db中
                           where s.商品編號 == itemid  //找尋符合itemid的值
                           select s).FirstOrDefault();  //撈取符合結果
                if (pro != default(Product)) //判斷資料庫中是否有該項商品，如有
                {
                    this.AddCartItem(pro);  //將當前物件pro加入購物清單中
                }
            }
            else  //如已在目前購物清單中
            {          finditem.購買數量++;          } //該商品數量+1
            return true;
        }

        private bool AddCartItem(Product pro)  //將初次購買商品加入購物車
        {
            var cartitem = new CartItem();  //將Product轉為CartItem
            {
                cartitem.商品編號 = pro.商品編號;
                cartitem.商品圖 = pro.商品圖;
                cartitem.商品名稱 = pro.商品名稱;
                cartitem.價格 = pro.價格;
                cartitem.購買數量 = 1;
            };
            this.buylist.Add(cartitem);  //將當前轉為CartItem型態的cartitem加入清單buylist中
            return true;
        }

        public bool Removeitem(int itemid)
        {
            var finditem = this.buylist.Where(s => s.商品編號 == itemid) //從buylist中找出符合該itemid的該項產品
                .Select(s=>s).FirstOrDefault();  
            if(finditem==default(CartItem))  //判斷是否在購物車中
            { }
            else { this.buylist.Remove(finditem); }  //如存在，則移除

            return true;
        }
        public bool Removeall()
        {
            this.buylist.Clear();  //將當前購物車清空
            return true;
        }

        public List<MyorderDetail> myorderdetailtolist(int orderid)  //將Cartitem類別轉為List<MyorderDetail>
        {
            var result = new List<MyorderDetail>();  //建立List<MyorderDetail>型態的物件實體result
            foreach (var cartItem in this.buylist)  //走訪目前購物車中所有物件
            {
                result.Add(new MyorderDetail() //依序將內容加入result中
                {
                    Name = cartItem.商品名稱,
                    price = cartItem.價格,
                    stuff = cartItem.購買數量,
                    MyorderId = orderid
                });
            }
            return result;  
        }


        public IEnumerator<CartItem> GetEnumerator()
        {
            return this.buylist.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.buylist.GetEnumerator();
        }
    }
}