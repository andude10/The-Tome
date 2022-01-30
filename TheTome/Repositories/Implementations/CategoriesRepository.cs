using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Models;
using TheTome.Repositories.Interfaces;

namespace TheTome.Repositories.Implementations;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public CategoriesRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Category FindCategory(string name)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var category = dbContext.Categories.FirstOrDefault(c => c.Name == name);

        if (category == null)
        {
            throw new KeyNotFoundException($"No category found with name {name}");
        }
        
        return category;
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

        if (category == null)
        {
            throw new KeyNotFoundException($"Category with {id.ToString()} id is Not found");
        }

        dbContext.Categories.Remove(category);
        dbContext.SaveChanges();
    }
}