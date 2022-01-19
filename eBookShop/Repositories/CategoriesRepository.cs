using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public CategoriesRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Category? FindCategory(string name)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Categories.FirstOrDefault(c => c.Name == name);
    }

    public void Create(Category item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Add(item);
        dbContext.SaveChanges();
    }

    public void Update(Category item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Update(item);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var category = dbContext.Categories.Find(id);

        if (category == null) return;

        dbContext.Categories.Remove(category);
        dbContext.SaveChanges();
    }
}