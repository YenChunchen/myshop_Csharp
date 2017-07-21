using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myshop.Models.Cart
{
    [Serializable]
    public class CartItem  //存放一種商品購買資訊
    {
        public int 商品編號 { get; set; }
        public string 商品圖 { get; set; }
        public string 商品名稱 { get; set; }
        public decimal 價格 { get; set; }
        public int 購買數量 { get; set; }
        public int 商品小計  //計算該項商品價錢總和
        {
            get { return (int)價格 * 購買數量; }    
        }
    }
}