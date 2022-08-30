using System;
using System.Collections.Generic;

namespace WebApplication6.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public List<ShopCartItem> Products { get; set; }
        public DateTime datePurchase { get; set; }
        public int Price { get; set; }
       
        
        

    }
}
