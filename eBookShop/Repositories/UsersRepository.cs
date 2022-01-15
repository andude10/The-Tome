using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public UsersRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public User? GetUser(string email)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? FindUser(string email, string password)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public void Create(User item)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Add(item);
        context.SaveChanges();
    }

    public void Update(User item)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Update(item);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        using var context = _contextFactory.CreateDbContext();

        var user = context.Users.Find(id);

        if (user == null) return;

        context.Users.Remove(user);
        context.SaveChanges();
    }
}