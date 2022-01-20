using eBookShop.Models;

namespace eBookShop.Repositories;

public interface IOrdersRepository
{
    Order? GetOrder(int id);
    void LoadBooks(Order order);
    void Create(Order item);
    void Update(Order item);
    void Delete(int id);
}