using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly AppDbContext _dbContext;

    public BooksRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _dbContext = contextFactory.CreateDbContext();
    }

    public Book? GetBook(int id)
    {
        return _dbContext.Books.Find(id);
    }

    public IEnumerable<Book> GetBooks()
    {
        return _dbContext.Books.ToList();
    }

    public void Create(Book item)
    {
        _dbContext.Add(item);
        _dbContext.SaveChanges();
    }

    public void Update(Book item)
    {
        _dbContext.Update(item);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = _dbContext.Books.Find(id);

        if (book == null) return;

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }

    #region Dispose interface
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if(!_disposed)
        {
            if(disposing)
            {
                _dbContext.Dispose();
            }
        }
        _disposed = true;
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}