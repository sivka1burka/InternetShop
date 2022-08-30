using Microsoft.AspNetCore.Http;

namespace WebApplication6.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public IFormFile Image { get; set; }
        public int SubcategoryId { get; set; }
        public int DiscountPercentage { get; set; }
        public byte[] ImgByte { get; set; }
        
    }
}
