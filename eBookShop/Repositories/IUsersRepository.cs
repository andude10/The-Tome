using eBookShop.Models;

namespace eBookShop.Repositories;

public interface IUsersRepository : IDisposable
{
    User? GetUser(string email);
    User? FindUser(string email, string password);
    void Create(User item);
    void Update(User item);
    void Delete(int id);
}