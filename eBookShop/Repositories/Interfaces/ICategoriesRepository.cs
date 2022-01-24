using eBookShop.Models;

namespace eBookShop.Repositories.Interfaces;

public interface ICategoriesRepository
{
    Category FindCategory(string name);
    void Create(Category item);
    void Update(Category item);
    void Delete(int id);
}