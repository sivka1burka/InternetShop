using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                
                
                User user = new User
                {
                    Name = model.Name,
                    Surname=model.Surname,
                    PhoneNumber = model.Phone,
                    UserName = model.Phone,
                    Age = model.Age
                };
                //добавление модели в бд
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl=null )
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Phone, model.Password,model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {

                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //выход из аккаунта
            await _signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "Home");
        }
        [HttpGet]
        public IActionResult UserPage()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ChangeUserViewModel changeUser=new ChangeUserViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.PhoneNumber,
                Age = user.Age,
            };
            
                
            return View(changeUser);
        }
        [HttpPost]
        public async Task<IActionResult> UserPage(ChangeUserViewModel model)
        {
            //получение модели аккаунта пользователя из бд
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            //обновление измененных данных контактных аккаунта
            user.Name = model.Name;
            user.Surname= model.Surname;
            user.Age = model.Age;
            user.PhoneNumber = model.Phone;
            var result = await _userManager.UpdateAsync(user);
            
            //изменение пароля
            if (model.Password != null)
            {
                result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                
            }
            if (result.Succeeded)
            {
                return View(model);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        public IActionResult OrdersHistory()
        {
            //получение модели аккаунта пользователя из бд
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            List<Order> orders = new List<Order>();
            //получение списка заказов пользователя
            orders=db.Orders.Where(o=>o.UserId==user.Id).ToList();
            return View(orders);
        }


    }
}
