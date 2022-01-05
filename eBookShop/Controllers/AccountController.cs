using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using eBookShop.ViewModels;
using eBookShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using eBookShop.Data;
using eBookShop.Repositories;

namespace eBookShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        public AccountController()
        {
            _usersRepository = new UsersRepository();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _usersRepository.FindUserAsync(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email);
 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login or (and) password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _usersRepository.FindUserAsync(model.Email, model.Password);
                if (user == null)
                {
                    _usersRepository.Create(new User { Email = model.Email, Password = model.Password, Name = model.Name });
                    await _usersRepository.SaveChangesAsync();
 
                    await Authenticate(model.Email);
 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or (and) password");
                }
            }
            return View(model);
        }
 
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}