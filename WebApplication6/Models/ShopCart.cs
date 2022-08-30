using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication6.Models
{
    public class ShopCart
    {
        private ApplicationContext db;
        public ShopCart(ApplicationContext context)
        {
            db = context;
        }

        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopItems { get; set; }

        public static ShopCart GetCart(IServiceProvider services)
        {
            //создание сессии
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context=services.GetService<ApplicationContext>();
            string shopCartId=session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId);
            return new ShopCart(context) { ShopCartId=shopCartId };
        }

        //добавление в корзину
        public void AddToCart(Product product)
        {
            ShopCartItem item = db.ShopCartItem.Include(s => s.product).Where(c => c.ShopCartId == ShopCartId && c.product.Id == product.Id).FirstOrDefault();
            if(item == null)
            {
                db.ShopCartItem.Add(new ShopCartItem
                {
                    ShopCartId = ShopCartId,
                    product = product,
                    Quantity = 1,
                    Price = product.TotalPrice,




                });
            }
            else
            {
                item.Quantity++;
                item.Price = product.TotalPrice * item.Quantity;


                


            }

           

            db.SaveChanges();
        }
        //увеличение кол-ва выбранного товара в корзине
        public void PlusToCart(Product product)
        {
            ShopCartItem item = db.ShopCartItem.Include(s => s.product).Where(c => c.ShopCartId == ShopCartId && c.product.Id == product.Id).FirstOrDefault();
            if (item.product.Amount > item.Quantity)
            {
                item.Quantity++;
                item.Price = product.TotalPrice * item.Quantity;
            }
            db.SaveChanges();
        }
        //уменьшение кол-ва выбранного товара в корзине
        public void MinusToCart(Product product)
        {
            ShopCartItem item = db.ShopCartItem.Include(s => s.product).Where(c => c.ShopCartId == ShopCartId && c.product.Id == product.Id).FirstOrDefault();
            if (item.Quantity!=1)
            {
                item.Quantity--;
                item.Price = product.TotalPrice * item.Quantity;
            }
            db.SaveChanges();
        }
        //удаление кол-ва выбранного товара в корзине
        public void RemoveFromCart(Product product)
        {

            ShopCartItem item= db.ShopCartItem.Include(s => s.product).Where(c => c.ShopCartId == ShopCartId && c.product.Id==product.Id).FirstOrDefault();
            db.ShopCartItem.Remove(item);
            db.SaveChanges();
        }
        //получение списка товаров корзины
        public List<ShopCartItem> GetShopItems()
        {
            return db.ShopCartItem.Where(c => c.ShopCartId == ShopCartId).Include(s => s.product).ToList();
        }
    }
}
