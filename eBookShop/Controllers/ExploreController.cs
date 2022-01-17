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
        
        var viewModel = new ExploreIndexViewModel
        {
            PageViewModel = new PageViewModel(_books.Count, pageId, PageSize),
            Books = _books.Skip((pageId - 1) * PageSize).Take(PageSize).ToList(),
            SortBookState = sortBookState
        };

        return View(viewModel);
    }

    public IActionResult BookViewer(int bookId)
    {
        var book = _booksRepository.GetBook(bookId);
        
        if (book == null)
        {
            return NotFound(book);
        }
        
        return View(new BookViewerViewModel(_usersRepository.GetUser(User.Identity.Name), book));
    }

    /// <summary>
    /// The SortBook returns a sorted list of books.
    /// </summary>
    /// <param name="sortBookState">Sort state</param>
    /// <returns></returns>
    private static IEnumerable<Book> SortBook(IEnumerable<Book> books, SortBookState sortBookState)
    {
        return sortBookState switch
        {
            //will soon
            SortBookState.Popular => books,
            //will soon
            SortBookState.HighRating => books,
            SortBookState.PriceAsc => books.OrderBy(b => b.Price),
            SortBookState.PriceDesc => books.OrderByDescending(b => b.Price),
            _ => books
        };
    }
}