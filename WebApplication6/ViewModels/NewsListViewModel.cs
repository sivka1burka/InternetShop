using System.Collections.Generic;
using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class NewsListViewModel
    {
        public List<News> newsList { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
