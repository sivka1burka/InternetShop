using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class ShopCartController : Controller
    {
        private ApplicationContext db;
        private readonly ShopCart _shopCart;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public ShopCartController(ApplicationContext context, ShopCart shopCart, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            db = context;
            _shopCart = shopCart;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpGet]
        public ViewResult ShopCart()
        {
            //получение списка продуктов находящихся в корзине
            var items = _shopCart.GetShopItems();
            _shopCart.listShopItems = items;

            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart
            };
            return View(obj);
        }
       
        //Добавление продукта в корзину по переданному id
        public RedirectToActionResult addToCart(int id)
        {
            //Получение модели продукта из базы данных
            var item = db.Products.FirstOrDefault(i=>i.Id==id);
            
            if (item != null)
            {
                //Добавление в корзину
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("ShopCart");
        }
        public RedirectToActionResult RemoveFromCart(int id)
        {
            //Получение модели продукта из базы данных
            var item = db.Products.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                //удаление продукта из корзины 
                _shopCart.RemoveFromCart(item);
            }

            


           
            return RedirectToAction("ShopCart");
        }
        //Увеличение кол-ва выбранного продукта в корзине
        public RedirectToActionResult PlusShopCartItem(int id)
        {
            var item = db.Products.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                _shopCart.PlusToCart(item);
            }
            return RedirectToAction("ShopCart");
        }
        //Уменьшение кол-ва выбранного продукта в корзине
        public RedirectToActionResult MinusShopCartItem(int id)
        {
            var item = db.Products.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                _shopCart.MinusToCart(item);
            }
            return RedirectToAction("ShopCart");
        }
        //Вывод представления с формой для заполнения сведений об контактах для получения заказа
        [HttpGet]
        public IActionResult Order()
        {
            Order order = new Order();
            //Если пользователь авторизирован в поля для заполнения вставляются его ФИО и номер телефона
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;                
                order.FullName = user.Name + " " + user.Surname;
                order.PhoneNumber = user.PhoneNumber;

            }
            //Получение списка корзины 
            order.Products = _shopCart.GetShopItems();
            //Расчет полной стоимости товаров в корзине
            order.Price = order.Products.Select(x => x.Price).Sum();
            return View(order);
        }
        [HttpPost]
        public RedirectToActionResult Order(Order order)
        {
            //Создание заказа с заполненными данными в представлении
            Order orderNew = new Order()
            {
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                Products = _shopCart.GetShopItems(),
                datePurchase = DateTime.Now,
                Price= _shopCart.GetShopItems().Select(x => x.Price).Sum()
            };
            //Если пользователь авторизирован, добавление в модель заказа Id пользователя для учета истории заказов в личном кабинете
            if (User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                orderNew.UserId = user.Id;
                

            }
            //добавление сведений о заказе в базу данных
            db.Orders.Add(orderNew);
            db.SaveChanges();
            //перенаправление на страницу о созданном заказе с передачей его Id
            return RedirectToAction("OrderStatus", new {@id=orderNew.Id });
        }

        [HttpGet]
        public IActionResult OrderStatus(int id)
        {
            //получение сведений о заказе
            Order order=db.Orders.Where(o=>o.Id == id).Include(o=>o.Products).ThenInclude(p=>p.product).First();
            //изменение кол-ва товара в базе данных после оформления заказа
            foreach (var prod in order.Products)
            {
                Product prodChange = db.Products.Where(p => p.Id == prod.product.Id).First();
                prodChange.Amount = prodChange.Amount - prod.Quantity;
            }
            db.SaveChanges();
            return View(order);
        }
    }
}
