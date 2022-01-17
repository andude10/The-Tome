using Castle.Core.Internal;
using eBookShop.Data;
using eBookShop.Models;
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
    
    [Authorize]
    public void AddBookToCart(int bookId)
    {
        var user = _usersRepository.GetUser(User.Identity.Name);
        var book = _booksRepository.GetBook(bookId);
        
        if (user.Orders.IsNullOrEmpty() || user.Orders.Last().IsCompleted)
        {
            var order = new Order() {User = user, OrderDate = DateTime.Now};
            order.Books.Add(book);
            user.Orders.Add(order);
        }
        else
        {
            user.Orders.Last().Books.Add(book);
        }
        
        _usersRepository.Update(user);
    }
}