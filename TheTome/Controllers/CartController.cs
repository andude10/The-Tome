using System.Diagnostics;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Models;
using TheTome.Repositories.Implementations;
using TheTome.Repositories.Interfaces;
using TheTome.ViewModels;

namespace TheTome.Controllers;

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
        _usersRepository.LoadLikedBooks(user);

        var cart = _usersRepository.GetLastOrder(user.Email);
        _ordersRepository.LoadBooks(cart);
        
        var cartViewModel = new CartViewModel
        {
            TotalPrice = cart.Books.Sum(b => b.Price),
            OrderId = cart.Id,
            BooksViewModel = new BooksViewModel(cart.Books, user.LikedBooks, null)
        };

        return View(cartViewModel);
    }
    
    [Authorize]
    public IActionResult BuyBookToggle(int bookId)
    {
        var user = _usersRepository.GetUser(User.Identity.Name);
        var lastOrder = _usersRepository.GetLastOrder(user.Email);
        var book = _booksRepository.GetBook(bookId);

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

    private void AddBookToCart(Book book, User user)
    {
        var lastOrder = _usersRepository.GetLastOrder(user.Email);
        
        if (lastOrder.IsCompleted)
        {
            var order = new Order { User = user, OrderDate = DateTime.Now };
            order.Books.Add(book);
            
            _usersRepository.LoadOrders(user);
            user.Orders.Add(order);
            
            _usersRepository.Update(user);
        }
        else
        {
            lastOrder.Books.Add(book);
            _ordersRepository.Update(lastOrder);
        }
    }
}