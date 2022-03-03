using Microsoft.EntityFrameworkCore;
using TheTome.Core.Models;
using TheTome.Core.RepositoriesInterfaces;
using TheTome.Infrastructure.Data;

namespace TheTome.Infrastructure.RepositoriesImplementations;

public class UsersRepository : IUsersRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public UsersRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    /// <summary>
    ///     GetUser returns a user WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>User WITHOUT associated data</returns>
    /// User must contain at least one Order
    public User GetUser(string email)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var user = dbContext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null) throw new KeyNotFoundException($"No user found with email {email}");

        return user;
    }

    public User GetUser(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var user = dbContext.Users.Find(id);

        if (user == null) throw new KeyNotFoundException($"No user found with id {id}");

        return user;
    }

    /// <summary>
    ///     Loads all liked books of the book
    /// </summary>
    public void LoadLikedBooks(User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        dbContext.Entry(user).State = EntityState.Unchanged;
        dbContext.Entry(user).Collection(u => u.LikedBooks).Load();
        dbContext.Entry(user).State = EntityState.Detached;
    }

    /// <summary>
    ///     Loads all user orders
    /// </summary>
    /// TODO: Explain what's going on here
    public void LoadOrders(User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        dbContext.Entry(user).State = EntityState.Unchanged;
        dbContext.Entry(user).Collection(u => u.Orders).Load();
        dbContext.Entry(user).State = EntityState.Detached;
    }

    /// <summary>
    ///     Loads all user posts
    /// </summary>
    public void LoadPosts(User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        dbContext.Entry(user).State = EntityState.Unchanged;
        dbContext.Entry(user).Collection(u => u.Posts).Load();
        dbContext.Entry(user).State = EntityState.Detached;
    }

    /// <summary>
    ///     Finds a user by name and password. Used for registration and authentication
    /// </summary>
    /// <param name="email">The user email</param>
    /// <param name="password">The user password</param>
    /// <returns></returns>
    public User? FindUser(string email, string password)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    /// <summary>
    ///     GetLastOrder returns a last user order WITHOUT associated data.
    ///     To load related data, you need to use the LoadList() in OrdersRepository
    /// </summary>
    /// <returns>Order WITHOUT associated data</returns>
    /// <param name="email">Email of the user whose last order will be returned</param>
    public Order GetLastOrder(string email)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var user = dbContext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null) throw new KeyNotFoundException($"User with {email} email is Not found");

        var order = dbContext.Entry(user)
            .Collection(u => u.Orders)
            .Query()
            .OrderBy(o => o.Id)
            .Last();
        if (order == null)
            throw new NullReferenceException(
                $"No user (user id: {user.Id}) order was found. User must have at least one order");

        return order;
    }

    public void Create(User item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Add(item);
        dbContext.SaveChanges();
    }

    public void Update(User item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Update(item);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var user = dbContext.Users.Find(id);

        if (user == null) throw new KeyNotFoundException($"User with {id.ToString()} id is Not found");

        dbContext.Users.Remove(user);
        dbContext.SaveChanges();
    }
}