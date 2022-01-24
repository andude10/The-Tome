using Castle.Core.Internal;
using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories.Implementations;
using eBookShop.Repositories.Interfaces;
using eBookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

public class MarketController : Controller
{
    private const int PageSize = 12;
    private readonly IBooksRepository _booksRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUsersRepository _usersRepository;
    private List<Book> _books;

    public MarketController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _booksRepository = new BooksRepository(contextFactory);
        _usersRepository = new UsersRepository(contextFactory);
        _ordersRepository = new OrdersRepository(contextFactory);
        _books = _booksRepository.GetBooks().ToList();
    }

    public IActionResult Catalog(int pageId = 1, SortBookState sortBookState = SortBookState.Popular)
    {
        _books = SortBook(_books, sortBookState).ToList();
        _books = _books.Skip((pageId - 1) * PageSize).Take(PageSize).ToList();

        var pageViewModel = new PageViewModel(_books.Count, pageId, PageSize);
        
        if (User.Identity is not {IsAuthenticated: true})
        {
            return View(new CatalogViewModel
            {
                PageViewModel = pageViewModel,
                BooksViewModel = new BooksViewModel(_books, null, null),
                SortBookState = sortBookState
            });
        }
        
        var user = _usersRepository.GetUser(User.Identity.Name);
        var cart = _usersRepository.GetLastOrder(user.Email);

        _usersRepository.LoadLikedBooks(user);
        _ordersRepository.LoadBooks(cart);

        return View(new CatalogViewModel
        {
            PageViewModel = pageViewModel,
            BooksViewModel = new BooksViewModel(_books, user.LikedBooks, cart.Books),
            SortBookState = sortBookState
        });
    }

    public IActionResult BookViewer(int bookId)
    {
        var book = _booksRepository.GetBook(bookId);
        var user = _usersRepository.GetUser(User.Identity?.Name);

        _usersRepository.LoadLikedBooks(user);
        var cart = _usersRepository.GetLastOrder(user.Email);

        return user.Orders.Last().Books.IsNullOrEmpty()
            ? View(new BookViewerViewModel(false, book))
            : View(new BookViewerViewModel(cart.Books.Exists(b => b.Id == book.Id), book));
    }

    /// <summary>
    ///     Current user likes book
    /// </summary>
    /// <param name="bookId">The book id</param>
    /// <returns></returns>
    [Authorize]
    public IActionResult GiveStarToBook(int bookId)
    {
        _booksRepository.GiveStarToBook(bookId, User.Identity.Name);
        return new NoContentResult();
    }

    /// <summary>
    ///     The SortBook returns a sorted list of books.
    /// </summary>
    /// <param name="books">The books</param>
    /// <param name="sortBookState">Sort state</param>
    /// <returns></returns>
    private IEnumerable<Book> SortBook(IEnumerable<Book> books, SortBookState sortBookState)
    {
        return sortBookState switch
        {
            SortBookState.Popular => books.OrderBy(b =>
            {
                _booksRepository.LoadBookOrders(b);
                return b.Orders.Count;
            }),
            SortBookState.HighRating => books.OrderBy(b => b.Stars),
            SortBookState.PriceAsc => books.OrderBy(b => b.Price),
            SortBookState.PriceDesc => books.OrderByDescending(b => b.Price),
            _ => books
        };
    }
}