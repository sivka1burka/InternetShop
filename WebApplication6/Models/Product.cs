﻿namespace WebApplication6.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public byte[] Image { get; set; }
        public int SubcategoryId { get; set; }
        public int DiscountPercentage { get; set; }
        public int TotalPrice { get; set; }
        
    }
}
