using eBookShop.Models;

namespace eBookShop.Repositories
{
    public interface ICategoriesRepository : IDisposable
    {
        Category? FindCategory(string name);
        void Create(Category item); 
        void Update(Category item); 
        void Delete(int id); 
        Task<int> SaveChangesAsync();
    }
}