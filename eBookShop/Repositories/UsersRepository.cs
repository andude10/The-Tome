using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _dbContext;

    public UsersRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _dbContext = contextFactory.CreateDbContext();
    }

    public User? GetUser(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public User? FindUser(string email, string password)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }

    public void Create(User item)
    {
        _dbContext.Add(item);
        _dbContext.SaveChanges();
    }

    public void Update(User item)
    {
        _dbContext.Update(item);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _dbContext.Users.Find(id);

        if (user == null) return;

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }
    
    #region Dispose interface
    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if(!_disposed)
        {
            if(disposing)
            {
                _dbContext.Dispose();
            }
        }
        _disposed = true;
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}