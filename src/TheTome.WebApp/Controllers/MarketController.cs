using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Core.Models;
using TheTome.Core.RepositoriesInterfaces;
using TheTome.Infrastructure.Data;
using TheTome.Infrastructure.RepositoriesImplementations;
using TheTome.WebApp.ViewModels;

namespace TheTome.WebApp.Controllers;

public class MarketController : Controller
{
    private const int PageSize = 12;
    private readonly IBooksRepository _booksRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUsersRepository _usersRepository;

    public MarketController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _booksRepository = new BooksRepository(contextFactory);
        _usersRepository = new UsersRepository(contextFactory);
        _ordersRepository = new OrdersRepository(contextFactory);
    }

    public IActionResult Catalog(int pageId = 1, SortBookState sortBookState = SortBookState.Popular)
    {
        var source = _booksRepository.GetBooks(pageId, PageSize, sortBookState).ToList();
        var pageViewModel = new PageViewModel(source.Count, pageId, PageSize);

        if (User.Identity is not {IsAuthenticated: true})
            return View(new CatalogViewModel(
                new BooksViewModel(source, null, null),
                pageViewModel,
                sortBookState
            ));

        var user = _usersRepository.GetUser(User.Identity.Name ?? throw new InvalidOperationException());
        var cart = _usersRepository.GetLastOrder(user.Email);

        _usersRepository.LoadLikedBooks(user);
        _ordersRepository.LoadBooks(cart);

        return View(new CatalogViewModel(
            new BooksViewModel(source, user.LikedBooks, cart.Books),
            pageViewModel,
            sortBookState
        ));
    }

    public IActionResult BookViewer(int bookId)
    {
        var book = _booksRepository.GetBook(bookId);

        if (User.Identity is not {IsAuthenticated: true}) return View(new BookViewerViewModel(false, false, book));

        var user = _usersRepository.GetUser(User.Identity.Name ?? throw new InvalidOperationException());
        var cart = _usersRepository.GetLastOrder(user.Email);

        _usersRepository.LoadLikedBooks(user);
        _ordersRepository.LoadBooks(cart);

        return View(new BookViewerViewModel(cart.Books.Exists(b => b.Id == bookId),
            user.LikedBooks.Exists(b => b.Id == bookId),
            book));
    }

    /// <summary>
    ///     Current user likes book
    /// </summary>
    /// <param name="bookId">The book id</param>
    /// <returns></returns>
    [Authorize]
    public IActionResult GiveStarToBook(int bookId)
    {
        _booksRepository.GiveStarToBook(bookId, User.Identity?.Name ?? throw new InvalidOperationException());
        return new NoContentResult();
    }
}