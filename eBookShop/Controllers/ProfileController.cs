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
        public ProfileController()
        {
            _usersRepository = new UsersRepository();
        }

        [Authorize]
        public async Task<IActionResult> Info()
        {
            var user = await _usersRepository.GetUserAsync(User.Identity.Name);

            if(user != null)
            {
                return View(user);
            }
            
            return NotFound();
        }

        public IActionResult ShowOrders()
        {
            return View(User);
        }
    }
}