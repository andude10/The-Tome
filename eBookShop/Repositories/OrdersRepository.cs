using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public OrdersRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Order? GetOrder(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Orders.Find(id);
        }
        public void Create(Order item) 
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(item);
            context.SaveChanges();
        } 
        public void Update(Order item)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Update(item);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var order = context.Orders.Find(id);

            if (order == null) return;
            
            context.Orders.Remove(order);
            context.SaveChanges();
        }
    }
}