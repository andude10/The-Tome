using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public CategoriesRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Category? FindCategory(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Categories.FirstOrDefault(c => c.Name == name);
        }
        public void Create(Category item) 
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(item);
            context.SaveChanges();
        } 
        public void Update(Category item)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Update(item);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var category = context.Categories.Find(id);

            if (category == null) return;
            
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}