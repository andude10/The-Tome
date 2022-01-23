using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories.Implementations;

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
    public User? GetUser(string email)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetUser(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Users.Find(id);
    }

    /// <summary>
    ///     Loads all liked books of the book
    /// </summary>
    /// TODO: Explain what's going on here
    public void LoadLikedBooks(User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var email = user.Email;
        
        // To use the Load() method and load an object's associated data,
        // the object must be created in the current context
        var userInContext = dbContext.Users.First(u => u.Email == email);

        dbContext.Entry(userInContext).Collection(u => u!.LikedBooks).Load();

        user.LikedBooks = userInContext.LikedBooks;
    }

    /// <summary>
    ///     Loads all user orders
    /// </summary>
    /// TODO: Explain what's going on here
    public void LoadOrders(User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var email = user.Email;
        var userInContext = dbContext.Users.First(u => u.Email == email);

        dbContext.Entry(userInContext).Collection(u => u!.Orders).Load();

        user.Orders = userInContext.Orders;
    }

    /// <summary>
    ///     Loads all user posts
    /// </summary>
    public void LoadPosts(User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var email = user.Email;
        
        // To use the Load() method and load an object's associated data,
        // the object must be created in the current context
        var userInContext = dbContext.Users.First(u => u.Email == email);

        dbContext.Entry(userInContext).Collection(u => u!.Posts).Load();

        user.Posts = userInContext.Posts;
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