using eBookShop.Models;

namespace eBookShop.Repositories;

public interface IUsersRepository
{
    /// <summary>
    /// GetUser returns a user WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>User WITHOUT associated data</returns>
    /// User must contain at least one Order
    User? GetUser(string email);
    
    /// <summary>
    /// Loads all liked books of the book
    /// </summary>
    void LoadLikedBooks(User user);
    
    /// <summary>
    /// Loads all user orders
    /// </summary>
    void LoadOrders(User user);
    
    /// <summary>
    /// Finds a user by name and password. Used for registration and authentication
    /// </summary>
    /// <param name="email">The user email</param>
    /// <param name="password">The user password</param>
    /// <returns></returns>
    User? FindUser(string email, string password);
    void Create(User item);
    void Update(User item);
    void Delete(int id);
}