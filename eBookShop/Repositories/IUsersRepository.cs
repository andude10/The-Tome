using eBookShop.Models;

namespace eBookShop.Repositories
{
    public interface IUsersRepository : IDisposable
    {
        User? GetUser(int id);
        void Create(User item); 
        void Update(User item); 
        void Delete(int id); 
    }
}