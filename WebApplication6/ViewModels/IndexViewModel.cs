using System.Collections.Generic;
using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class IndexViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public List<Product> products { get; set; }
        public string CategoryName { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public int categoryId { get; set; }
        public int subcategoryId { get; set; }
    }
}
