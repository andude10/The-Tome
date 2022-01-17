using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly AppDbContext _dbContext;

    public CategoriesRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _dbContext = contextFactory.CreateDbContext();
    }

    public Category? FindCategory(string name)
    {
        return _dbContext.Categories.FirstOrDefault(c => c.Name == name);
    }

    public void Create(Category item)
    {
        _dbContext.Add(item);
        _dbContext.SaveChanges();
    }

    public void Update(Category item)
    {
        _dbContext.Update(item);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var category = _dbContext.Categories.Find(id);

        if (category == null) return;

        _dbContext.Categories.Remove(category);
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