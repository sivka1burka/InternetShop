﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class ShopCartItem
    {
        
        public int Id { get; set; }
        public Product product { get; set; }
        public int Price { get; set; }
        public string ShopCartId { get; set; }
        public int Quantity { get; set; }
        
        
     

        

        
    }
}
