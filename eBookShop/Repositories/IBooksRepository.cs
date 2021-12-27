using eBookShop.Models;

namespace eBookShop.Repositories
{
    public interface IBooksRepository : IDisposable
    {
        Book? GetBook(int id);
        void Create(Book item); 
        void Update(Book item); 
        void Delete(int id); 
    }
}