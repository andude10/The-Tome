using TheTome.Core.Models;

namespace TheTome.Core.RepositoriesInterfaces;

public interface IUsersRepository
{
    /// <summary>
    ///     GetUser returns a user WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>User WITHOUT associated data</returns>
    /// User must contain at least one Order
    User GetUser(string email);

    /// <summary>
    ///     GetUser returns a user WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>User WITHOUT associated data</returns>
    /// User must contain at least one Order
    User GetUser(int id);

    /// <summary>
    ///     Loads all liked books of the book
    /// </summary>
    void LoadLikedBooks(User user);

    /// <summary>
    ///     Loads all user orders
    /// </summary>
    void LoadOrders(User user);

    /// <summary>
    ///     Loads all user posts
    /// </summary>
    void LoadPosts(User user);

    /// <summary>
    ///     Finds a user by name and password. Used for registration and authentication
    /// </summary>
    /// <param name="email">The user email</param>
    /// <param name="password">The user password</param>
    /// <returns></returns>
    User? FindUser(string email, string password);

    /// <summary>
    ///     GetLastOrder returns a last user order WITHOUT associated data.
    ///     To load related data, you need to use the LoadList() in OrdersRepository
    /// </summary>
    /// <returns>Order WITHOUT associated data</returns>
    /// <param name="email">Email of the user whose last order will be returned</param>
    Order GetLastOrder(string email);

    void Create(User item);
    void Update(User item);
    void Delete(int id);
}