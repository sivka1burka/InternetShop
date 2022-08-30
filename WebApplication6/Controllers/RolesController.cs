using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            if (User.IsInRole("admin"))
            {
                return View(_roleManager.Roles.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        //Страница создания роли
        public IActionResult Create()
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
        public async Task<IActionResult> Create(string name)
        {
            if (User.IsInRole("admin"))
            {
                if (!string.IsNullOrEmpty(name))
                {
                    //Создание роли с переданным в параметрах именем
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("HomePage");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return View(name);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (User.IsInRole("admin"))
            {
                //Получаем роль по id
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    //удаление роли
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        //Получение списка всех пользователей
        public IActionResult UserList(string searchString)
        {
            if (User.IsInRole("admin"))
            {
                List<User> users=new List<User>();
                users = _userManager.Users.ToList();
               
                if (!String.IsNullOrEmpty(searchString))
                {
                    users = users.Where(p => p.PhoneNumber.Contains(searchString)).ToList();
                }
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        } 

        public async Task<IActionResult> Edit(string userId)
        {
            if (User.IsInRole("admin"))
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var allRoles = _roleManager.Roles.ToList();
                    ChangeRoleViewModel model = new ChangeRoleViewModel
                    {
                        UserId = user.Id,
                        UserPhone = user.PhoneNumber,
                        UserRoles = userRoles,
                        AllRoles = allRoles
                    };
                    return View(model);
                }
                return NotFound();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
      
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            if (User.IsInRole("admin"))
            {
                // получаем пользователя
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    // получаем все роли
                    var allRoles = _roleManager.Roles.ToList();
                    // получаем список ролей, которые были добавлены
                    var addedRoles = roles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    return RedirectToAction("UserList");
                }

                return NotFound();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
    }    
}
