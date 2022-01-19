using System.Diagnostics;
using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    private readonly UsersRepository _usersRepository;

    public BooksRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _usersRepository = new UsersRepository(contextFactory);
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
    public void LoadBookOrders(ref Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var id = book.Id;
        book = dbContext.Books.First(u => u.Id == id);
        
        dbContext.Entry(book).Collection(b => b!.Orders).Load();
    }
    
    /// <summary>
    /// Loads all users who liked the book
    /// </summary>
    public void LoadUsersWhoLike(ref Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var id = book.Id;
        book = dbContext.Books.First(u => u.Id == id);

        dbContext.Entry(book).Collection(b => b!.UsersWhoLike).Load();
    }
    
    /// <summary>
    /// Loads all categories the book belongs to
    /// </summary>
    public void LoadCategories(ref Book book)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var id = book.Id;
        book = dbContext.Books.First(u => u.Id == id);

        dbContext.Entry(book).Collection(b => b!.Categories).Load();
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

        if (book == null)
        {
            return;
        }
        
        var user = _usersRepository.GetUser(email);
        Debug.Assert(user != null, nameof(user) + " != null");
        
        if (user.LikedBooks.Any(b => b.Id == bookId))
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