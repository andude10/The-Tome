using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly AppDbContext _dbContext;
    public OrdersRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _dbContext = contextFactory.CreateDbContext();
    }

    public Order? GetOrder(int id)
    {
        return _dbContext.Orders.Find(id);
    }

    public void Create(Order item)
    {
        _dbContext.Add(item);
        _dbContext.SaveChanges();
    }

    public void Update(Order item)
    {
        _dbContext.Update(item);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var order = _dbContext.Orders.Find(id);

        if (order == null) return;

        _dbContext.Orders.Remove(order);
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