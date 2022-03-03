using TheTome.Core.Models;

namespace TheTome.Core.RepositoriesInterfaces;

public interface ICategoriesRepository
{
    Category FindCategory(string name);
    void Create(Category item);
    void Update(Category item);
    void Delete(int id);
}