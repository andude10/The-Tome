using System.Diagnostics;
using Castle.Core.Internal;
using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using eBookShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

public class ExploreController : Controller
{
    private const int PageSize = 12;
    private readonly IBooksRepository _booksRepository;
    private readonly IUsersRepository _usersRepository;
    private List<Book> _books;

    public ExploreController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _booksRepository = new BooksRepository(contextFactory);
        _usersRepository = new UsersRepository(contextFactory);
        _books = _booksRepository.GetBooks().ToList();
    }

    public IActionResult Index(int pageId = 1, SortBookState sortBookState = SortBookState.Popular)
    {
        _books = SortBook(_books, sortBookState).ToList();
        _books = _books.Skip((pageId - 1) * PageSize).Take(PageSize).ToList();

        var user = _usersRepository.GetUser(User.Identity.Name);
        Debug.Assert(user != null, nameof(user) + " != null");
        _usersRepository.LoadLikedBooks(ref user);
        
        var viewModel = new ExploreIndexViewModel
        {
            PageViewModel = new PageViewModel(_books.Count, pageId, PageSize),
            CatalogViewModel = new CatalogViewModel(_books, user.LikedBooks),
            SortBookState = sortBookState
        };

        return View(viewModel);
    }

    public IActionResult BookViewer(int bookId)
    {
        var book = _booksRepository.GetBook(bookId);
        var user = _usersRepository.GetUser(User.Identity.Name);
        
        if (user == null || book == null)
        {
            return NotFound();
        }
        
        _usersRepository.LoadLikedBooks(ref user);
        _usersRepository.LoadOrders(ref user);

        return user.Orders.Last().Books.IsNullOrEmpty()
            ? View(new BookViewerViewModel(false, book))
            : View(new BookViewerViewModel(user.Orders.Last().Books.Exists(b => b.Id == book.Id), book));
    }

    /// <summary>
    /// The GiveStarToBook give a star to book
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public IActionResult GiveStarToBook(int bookId)
    {
        _booksRepository.GiveStarToBook(bookId, User.Identity.Name);
        return new EmptyResult();
    }

    /// <summary>
    /// The SortBook returns a sorted list of books.
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
                _booksRepository.LoadBookOrders(ref b);
                return b.Orders.Count;
            }),
            SortBookState.HighRating => books.OrderBy(b => b.Stars),
            SortBookState.PriceAsc => books.OrderBy(b => b.Price),
            SortBookState.PriceDesc => books.OrderByDescending(b => b.Price),
            _ => books
        };
    }
}