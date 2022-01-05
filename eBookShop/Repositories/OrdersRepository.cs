using eBookShop.Data;
using eBookShop.Models;

namespace eBookShop.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _dbContext;

        public OrdersRepository()
        {
            _dbContext = new AppDbContext();
        }

        public Order? GetOrder(int id)
        {
            return _dbContext.Orders.Find(id);
        }
        public void Create(Order item) 
        {
            _dbContext.Add(item);
        } 
        public void Update(Order item)
        {
            _dbContext.Update(item);
        }
        public void Delete(int id)
        {
            var order = _dbContext.Orders.Find(id);

            if(order != null)
            {
                _dbContext.Orders.Remove(order);
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