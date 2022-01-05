using eBookShop.Data;
using eBookShop.Models;

namespace eBookShop.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoriesRepository()
        {
            _dbContext = new AppDbContext();
        }

        public Category? GetCategory(int id)
        {
            return _dbContext.Categories.Find(id);
        }
        public void Create(Category item) 
        {
            _dbContext.Add(item);
        } 
        public void Update(Category item)
        {
            _dbContext.Update(item);
        }
        public void Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if(category != null)
            {
                _dbContext.Categories.Remove(category);
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