using eBookShop.Models;

namespace eBookShop.Repositories
{
    public interface IUsersRepository : IDisposable
    {
        User? GetUser(int id);
        Task<User?> GetUserAsync(string email);
        Task<User?> FindUserAsync(string email, string password);
        void Create(User item); 
        void Update(User item); 
        void Delete(int id); 
        Task<int> SaveChangesAsync();
    }
}