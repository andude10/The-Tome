using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBookShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IOrdersRepository _ordersRepository;

        public CartController(AppDbContext db)
        {
            _booksRepository = new BooksRepository(db);
            _usersRepository = new UsersRepository(db);
            _ordersRepository = new OrdersRepository(db);
        }
        
        [Authorize]
        public async Task<IActionResult> CartSummary()
        {
            var user = await _usersRepository.GetUserAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var result = user.Orders.Last().Books;

            return View(result);
        }
    }    
}
