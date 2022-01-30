using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Models;
using TheTome.Repositories.Interfaces;

namespace TheTome.Repositories.Implementations;

public class BooksRepository : IBooksRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public BooksRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    /// <summary>
    ///     GetBook returns a book WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>Book WITHOUT associated data</returns>
    public Book GetBook(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var book = dbContext.Books.Find(id);

        if (book == null)
        {
            throw new KeyNotFoundException($"No book found with id {id}");
        }
        
        return book;
    }
    
    /// <summary>
    ///     Returns a list of books with no associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Book> GetBooks()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Books.ToList();
    }
    
    /// <summary>
    ///     Returns a list of books with no associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Book> GetBooks(int skipSize, int takeSize, SortBookState sortBookState)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var result = dbContext.Books.Skip(skipSize)
            .Take(takeSize)
            .ToList();

        return SortBook(sortBookState, result);
    }

    /// <summary>
    ///     Loads all orders of the book
    /// </summary>
    public void LoadBookOrders(Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        dbContext.Entry(book).State = EntityState.Unchanged;
        dbContext.Entry(book).Collection(b => b.Orders).Load();
        dbContext.Entry(book).State = EntityState.Detached;
    }

    /// <summary>
    ///     Loads all users who liked the book
    /// </summary>
    public void LoadUsersWhoLike(Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        dbContext.Entry(book).State = EntityState.Unchanged;
        dbContext.Entry(book).Collection(b => b.UsersWhoLike).Load();
        dbContext.Entry(book).State = EntityState.Detached;
    }

    /// <summary>
    ///     Loads all categories the book belongs to
    /// </summary>
    public void LoadCategories(Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        dbContext.Entry(book).State = EntityState.Unchanged;
        dbContext.Entry(book).Collection(b => b.Categories).Load();
        dbContext.Entry(book).State = EntityState.Detached;
    }

    /// <summary>
    ///     The GiveStarToBook give a star to book
    /// </summary>
    /// <param name="email">Email of the user who likes</param>
    /// <param name="bookId">The book id</param>
    public void GiveStarToBook(int bookId, string email)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var book = dbContext.Books.Find(bookId);
        var user = dbContext.Users.First(u => u.Email == email);

        if (book == null || user == null)
        {
            throw new KeyNotFoundException($"Book with id {bookId.ToString()} of user or email address {email} (or both) not found");   
        }

        dbContext.Entry(user).Collection(u => u!.LikedBooks).Load();

        if (user.LikedBooks.Exists(b => b.Id == bookId))
        {
            book.Stars -= 1;
            book.UsersWhoLike.Remove(user);
        }
        else
        {
            book.Stars += 1;
            book.UsersWhoLike.Add(user);
        }

        dbContext.Update(book);
        dbContext.SaveChanges();
    }

    public void Create(Book item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Add(item);
        dbContext.SaveChanges();
    }

    public void Update(Book item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Update(item);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var book = dbContext.Books.Find(id);

        if (book == null)
        {
            throw new KeyNotFoundException(id.ToString());
        }

        dbContext.Books.Remove(book);
        dbContext.SaveChanges();
    }
    
    /// <summary>
    ///     The SortBook returns a sorted list of books.
    /// </summary>
    /// <param name="books">The books</param>
    /// <param name="sortBookState">Sort state</param>
    /// <returns></returns>
    private IEnumerable<Book> SortBook(SortBookState sortBookState, IEnumerable<Book> books)
    {
        return sortBookState switch
        {
            SortBookState.Popular => books.OrderBy(b =>
            {
                LoadBookOrders(b);
                return b.Orders.Count;
            }),
            SortBookState.HighRating => books.OrderBy(b => b.Stars),
            SortBookState.PriceAsc => books.OrderBy(b => b.Price),
            SortBookState.PriceDesc => books.OrderByDescending(b => b.Price),
            _ => books
        };
    }
}