using System.Diagnostics;
using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public BooksRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    /// <summary>
    /// GetBook returns a book WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>Book WITHOUT associated data</returns>
    public Book? GetBook(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Books.Find(id);
    }
    
    /// <summary>
    /// Loads all orders of the book
    /// </summary>
    public void LoadBookOrders(Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var id = book.Id;
        var bookInContext = dbContext.Books.First(u => u.Id == id);
        
        dbContext.Entry(bookInContext).Collection(b => b!.Orders).Load();

        book.Orders = bookInContext.Orders;
    }
    
    /// <summary>
    /// Loads all users who liked the book
    /// </summary>
    public void LoadUsersWhoLike(Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var id = book.Id;
        var bookInContext = dbContext.Books.First(u => u.Id == id);
        
        dbContext.Entry(bookInContext).Collection(b => b!.UsersWhoLike).Load();

        book.UsersWhoLike = bookInContext.UsersWhoLike;
    }
    
    /// <summary>
    /// Loads all categories the book belongs to
    /// </summary>
    public void LoadCategories(Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var id = book.Id;
        var bookInContext = dbContext.Books.First(u => u.Id == id);
        
        dbContext.Entry(bookInContext).Collection(b => b!.Categories).Load();

        book.Categories = bookInContext.Categories;
    }
    
    /// <summary>
    /// Returns a list of books with no associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Book> GetBooks()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Books.ToList();
    }

    /// <summary>
    /// The GiveStarToBook give a star to book
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
            return;
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

        if (book == null) return;

        dbContext.Books.Remove(book);
        dbContext.SaveChanges();
    }
}