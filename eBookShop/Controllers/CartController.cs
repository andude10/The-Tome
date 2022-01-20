using System.Diagnostics;
using System.Linq;
using Castle.Core.Internal;
using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using eBookShop.ViewModels;
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
        var user = _usersRepository.GetUser(User.Identity.Name);
        Debug.Assert(user != null, nameof(user) + " != null");
        _usersRepository.LoadOrders(user);
        _usersRepository.LoadLikedBooks( user);

        var cart = user.Orders.Last();
        _ordersRepository.LoadBooks(cart);
        var books = cart.Books;

        var cartVm = new CartViewModel()
        {
            TotalPrice = books.Sum(b => b.Price),
            OrderId = user.Orders.Last().Id,
            CatalogViewModel = new CatalogViewModel(books, user.LikedBooks)
        };

        return View(cartVm);
    }
    
    [Authorize]
    public void AddBookToCart(int bookId)
    {
        var user = _usersRepository.GetUser(User.Identity.Name);
        Debug.Assert(user != null, nameof(user) + " != null");
        _usersRepository.LoadOrders(user);
        
        var book = _booksRepository.GetBook(bookId);

        var cart = user.Orders.Last();
        
        if (user.Orders.IsNullOrEmpty() || cart.IsCompleted)
        {
            var order = new Order() { User = user, OrderDate = DateTime.Now };
            order.Books.Add(book);
            user.Orders.Add(order);
        }
        else
        {
            _ordersRepository.LoadBooks(cart);
            cart.Books.Add(book);
        }
        
        _usersRepository.Update(user);
    }
}