using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Repositories.Implementations;
using TheTome.Repositories.Interfaces;
using TheTome.ViewModels;

namespace TheTome.Controllers;

public class CartController : Controller
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUsersRepository _usersRepository;

    public CartController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _usersRepository = new UsersRepository(contextFactory);
        _ordersRepository = new OrdersRepository(contextFactory);
    }

    [Authorize]
    public IActionResult CartSummary()
    {
        var user = _usersRepository.GetUser(User.Identity?.Name ?? throw new InvalidOperationException());
        _usersRepository.LoadLikedBooks(user);

        var cart = _usersRepository.GetLastOrder(user.Email);
        _ordersRepository.LoadBooks(cart);

        var cartViewModel = new CartViewModel(new BooksViewModel(cart.Books, user.LikedBooks, cart.Books))
        {
            TotalPrice = cart.Books.Sum(b => b.Price),
            OrderId = cart.Id
        };

        return View(cartViewModel);
    }

    [Authorize]
    public IActionResult BuyBookToggle(int bookId)
    {
        var user = _usersRepository.GetUser(User.Identity?.Name ?? throw new InvalidOperationException());
        var lastOrder = _usersRepository.GetLastOrder(user.Email);

        _ordersRepository.LoadBooks(lastOrder);

        if (!lastOrder.Books.Exists(b => b.Id == bookId))
        {
            _ordersRepository.AddBookToOrder(lastOrder.Id, bookId, user.Id);
        }
        else
        {
            _ordersRepository.RemoveBookFromOrder(lastOrder.Id, bookId);
        }

        return new NoContentResult();
    }
}