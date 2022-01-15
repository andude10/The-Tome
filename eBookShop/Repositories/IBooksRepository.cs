using eBookShop.Models;

namespace eBookShop.Repositories;

public interface IBooksRepository
{
    Book? GetBook(int id);
    IEnumerable<Book> GetBooks();
    void Create(Book item);
    void Update(Book item);
    void Delete(int id);
}