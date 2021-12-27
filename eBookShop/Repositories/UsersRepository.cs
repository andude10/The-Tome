using eBookShop.Data;
using eBookShop.Models;

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

            if(user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            } 
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