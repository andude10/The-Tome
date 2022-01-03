using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User? GetUser(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> FindUserAsync(string email, string password)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public void Create(User item) 
        {
            _dbContext.Add(item);
        } 
        public void Update(User item)
        {
            _dbContext.Update(item);
        }
        public void Delete(int id)
        {
            var user = _dbContext.Users.Find(id);

            if(user != null)
            {
                _dbContext.Users.Remove(user);
            } 
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}