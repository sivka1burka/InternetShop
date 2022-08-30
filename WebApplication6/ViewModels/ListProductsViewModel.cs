using System.Collections.Generic;
using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class ListProductsViewModel
    {
        public List<Product> products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
