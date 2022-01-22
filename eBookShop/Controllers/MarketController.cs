using Castle.Core.Internal;
using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories.Implementations;
using eBookShop.Repositories.Interfaces;
using eBookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        List<Book> likedBooks = null;
        if (User.Identity is {IsAuthenticated: false})
        {
            var user = _usersRepository.GetUser(User.Identity.Name);
            _usersRepository.LoadLikedBooks(user);
            likedBooks = user.LikedBooks;
        }

        var viewModel = new CatalogViewModel
        {
            PageViewModel = new PageViewModel(_books.Count, pageId, PageSize),
            BooksViewModel = new BooksViewModel(_books, likedBooks),
            SortBookState = sortBookState
        };

        return View(viewModel);
    }

    public IActionResult BookViewer(int bookId)
    {
        var book = _booksRepository.GetBook(bookId);
        var user = _usersRepository.GetUser(User.Identity?.Name);

        if (user == null || book == null) return NotFound();

        _usersRepository.LoadLikedBooks(user);
        _usersRepository.LoadOrders(user);

        var cart = user.Orders.Last();
        _ordersRepository.LoadBooks(cart);

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