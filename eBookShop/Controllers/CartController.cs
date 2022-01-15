using eBookShop.Data;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

public class CartController : Controller
{
    private readonly IBooksRepository _booksRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUsersRepository _usersRepository;

    public CartController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _booksRepository = new BooksRepository(contextFactory);
        _usersRepository = new UsersRepository(contextFactory);
        _ordersRepository = new OrdersRepository(contextFactory);
    }

    [Authorize]
    public IActionResult CartSummary()
    {
        var user = _usersRepository.GetUser("testEmail@gmail.com");

        if (user == null) return NotFound();

        var result = user.Orders.Last().Books;

        return View(result);
    }
}