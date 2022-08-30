using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;


namespace WebApplication6.Components
{
    
    public class MultiClassViewComponent : ViewComponent
    {
        private ApplicationContext db;
        public MultiClassViewComponent(ApplicationContext context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            MultiClass multiClass = new MultiClass();
            multiClass.products = db.Products.ToList();
            multiClass.subcategories = db.Subcategories.ToList();
            multiClass.categories = db.Categories.ToList();
            return View(multiClass);
        }
       
    }
}
