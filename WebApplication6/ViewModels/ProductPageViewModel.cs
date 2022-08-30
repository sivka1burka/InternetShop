using System.Collections.Generic;
using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class ProductPageViewModel
    {
        public Product product { get; set; }
        public List<Review> reviews { get; set; }
        public Review NewReview { get; set; }
    }
}
