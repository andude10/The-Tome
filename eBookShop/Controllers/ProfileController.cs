using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eBookShop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        public ProfileController(AppDbContext db)
        {
            _usersRepository = new UsersRepository(db);
        }

        [Authorize]
        public async Task<IActionResult> ProfileSettings()
        {
            //TODO: tomorrow
            return Content(User.Identity.Name);
            /*User user = await _usersRepository.FindUserAsync(User.Identity.Name);

            if(user != null)
            {
                return View(user);
            }
            
            return RedirectToAction("Index", "Home");
            */
        }

        public IActionResult ShowOrders()
        {
            return View(User);
        }
    }
}