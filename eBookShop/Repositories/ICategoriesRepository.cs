using eBookShop.Models;

namespace eBookShop.Repositories
{
    public interface ICategoriesRepository : IDisposable
    {
        Category? GetCategory(int id);
        void Create(Category item); 
        void Update(Category item); 
        void Delete(int id); 
    }
}