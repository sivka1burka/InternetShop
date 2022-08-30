using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private ApplicationContext db;
        public AdminController(ILogger<AdminController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
            db.SaveChanges();
        }
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

           
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Subcategories = db.Subcategories.ToList().OrderBy(s => s.CategoryId);
                //ViewBag.Subcategories = new SelectList(db.Subcategories.ToList().OrderBy(s => s.CategoryId), Subcategory.Id, );
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel pvm)
        {
            if (User.IsInRole("admin"))
            {
                Product product = new Product { Name = pvm.Name, Description = pvm.Description, Price = pvm.Price, Amount = pvm.Amount, SubcategoryId = pvm.SubcategoryId, DiscountPercentage = pvm.DiscountPercentage };
                if (pvm.Image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(pvm.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)pvm.Image.Length);
                    }
                    // установка массива байтов
                    product.Image = imageData;
                    product.TotalPrice = product.Price - (product.Price * product.DiscountPercentage / 100);
                }
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("ListProducts");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            
        }
        
        public IActionResult ListProducts(string searchString, int page = 1)
        {
            if (User.IsInRole("admin"))
            {
                List<Product> products = new List<Product>();
                products = db.Products.ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    products = products.Where(p => p.Name.Contains(searchString)).ToList();
                }

                int pageSize = 10;
                var count = products.Count();
                //Разбиение списка продуктов по 15 на страницу
                var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                ListProductsViewModel LPViewModel = new ListProductsViewModel();
                LPViewModel.products = items;
                LPViewModel.PageViewModel = pageViewModel;

                return View(LPViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        public IActionResult DeleteProduct(int idProduct)
        {
            if (User.IsInRole("admin"))
            {
                Product product = db.Products.SingleOrDefault(p => p.Id == idProduct);
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("ListProducts");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        [HttpGet]
        public IActionResult EditProduct(int idProduct)
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Subcategories = db.Subcategories.ToList().OrderBy(s => s.CategoryId);

                Product product = db.Products.SingleOrDefault(p => p.Id == idProduct);
                ProductViewModel PVM = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Amount = product.Amount,
                    SubcategoryId = product.SubcategoryId,
                    DiscountPercentage = product.DiscountPercentage,
                    ImgByte = product.Image

                };
                return View(PVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
            
            
            
            

            
        }
        [HttpPost]
        public IActionResult EditProduct(ProductViewModel pvm)
        {
            if (User.IsInRole("admin"))
            {
                Product product = new Product { Name = pvm.Name, Description = pvm.Description, Price = pvm.Price, Amount = pvm.Amount, SubcategoryId = pvm.SubcategoryId, DiscountPercentage = pvm.DiscountPercentage };
                Product prod = db.Products.SingleOrDefault(p => p.Id == pvm.Id);
                if (pvm.Image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(pvm.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)pvm.Image.Length);
                    }
                    // установка массива байтов
                    prod.Image = imageData;

                }



                prod.Name = pvm.Name;
                prod.Description = pvm.Description;
                prod.Price = pvm.Price;
                prod.Amount = pvm.Amount;
                prod.SubcategoryId = pvm.SubcategoryId;
                prod.DiscountPercentage = pvm.DiscountPercentage;

                prod.TotalPrice = prod.Price - (prod.Price * prod.DiscountPercentage / 100);
                db.SaveChanges();


                return RedirectToAction("ListProducts");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
           
        }
        public IActionResult ListOrders(string searchString)
        {
            if (User.IsInRole("admin"))
            {
                List<Order> orders = new List<Order>();
                orders = db.Orders.ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    orders = orders.Where(o=>o.Id==Convert.ToInt32(searchString)).ToList();
                }
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        [HttpGet]
        public IActionResult AddNews()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult AddNews(NewsViewModel nvm)
        {
            if (User.IsInRole("admin"))
            {
                News news = new News {Name=nvm.Name, Description=nvm.Description};
                news.PublicationDate=DateTime.Now;
                if (nvm.Image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(nvm.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)nvm.Image.Length);
                    }
                    // установка массива байтов
                    news.Image = imageData;
                    
                }
                db.NewsList.Add(news);
                db.SaveChanges();
                return RedirectToAction("ListNews");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult ListNews(string searchString, int page = 1)
        {
            if (User.IsInRole("admin"))
            {
                List<News> newsList = new List<News>();
                newsList = db.NewsList.ToList();
                
                if (!String.IsNullOrEmpty(searchString))
                {
                    newsList = newsList.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
                }

                int pageSize = 10;
                var count = newsList.Count();
                var items = newsList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                NewsListViewModel NLViewModel = new NewsListViewModel();
                NLViewModel.newsList = items;
                NLViewModel.PageViewModel = pageViewModel;

                return View(NLViewModel);                
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpGet]
        public IActionResult EditNews(int idNews)
        {
            if (User.IsInRole("admin"))
            {
                

                News news = db.NewsList.SingleOrDefault(n => n.Id == idNews);
                NewsViewModel NVM = new NewsViewModel()
                {
                    Id = news.Id,
                    Name = news.Name,
                    Description = news.Description,                  
                    ImgByte = news.Image,
                    PublicationDate= news.PublicationDate,
                };
                return View(NVM);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult EditNews(NewsViewModel nvm)
        {
            if (User.IsInRole("admin"))
            {
                News news = new News { Name = nvm.Name, Description = nvm.Description,PublicationDate=nvm.PublicationDate};
                News news1 = db.NewsList.SingleOrDefault(n => n.Id == nvm.Id);
                if (nvm.Image != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(nvm.Image.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)nvm.Image.Length);
                    }
                    // установка массива байтов
                    news1.Image = imageData;

                }



                news1.Name = nvm.Name;
                news1.Description = nvm.Description;
                news1.PublicationDate = DateTime.Now;
               
                db.SaveChanges();


                return RedirectToAction("ListNews");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public IActionResult DeleteNews(int idNews)
        {
            if (User.IsInRole("admin"))
            {
                News news = db.NewsList.SingleOrDefault(n => n.Id == idNews);
                db.NewsList.Remove(news);
                db.SaveChanges();
                return RedirectToAction("ListNews");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
