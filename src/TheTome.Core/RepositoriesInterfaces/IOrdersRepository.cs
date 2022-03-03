using TheTome.Core.Models;

namespace TheTome.Core.RepositoriesInterfaces;

public interface IOrdersRepository
{
    Order GetOrder(int id);
    void LoadBooks(Order order);
    void AddBookToOrder(int orderId, int bookId, int userId);
    void RemoveBookFromOrder(int orderId, int bookId);
    void LoadBook(Order order, Predicate<Book> predicate);
    void Create(Order item);
    void Update(Order item);
    void Delete(int id);
}