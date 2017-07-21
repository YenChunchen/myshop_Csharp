using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myshop.Models
{
    public class Order
    {
        [Required]
        public string 收貨人姓名 { set; get; }
        [Required]
        [Phone]
        public string 收貨人電話 { set; get; }
        [Required]
        public string 收貨人地址 { set; get; }
    }

}