using System.Collections.Generic;

namespace WebApplication6.Models
{
    public class MultiClass
    {
        public List<Product> products { get; set; }       
        public List<Subcategory> subcategories { get; set; }
        public List<Category> categories { get; set; }
        public List<News> newsList { get; set; }
    }
}
