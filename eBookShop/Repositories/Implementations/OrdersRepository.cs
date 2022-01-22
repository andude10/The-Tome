using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories.Implementations;

public class OrdersRepository : IOrdersRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public OrdersRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    /// <summary>
    ///     GetOrder returns a order WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>Order WITHOUT associated data</returns>
    public Order? GetOrder(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Orders.Find(id);
    }

    /// <summary>
    ///     Loads all books in an order
    /// </summary>
    public void LoadBooks(Order order)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var id = order.Id;
        var orderInContext = dbContext.Orders.First(o => o.Id == id);
        dbContext.Entry(orderInContext).Collection(o => o!.Books).Load();

        order.Books = orderInContext.Books;
    }

    public void Create(Order order)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Add(order);
        dbContext.SaveChanges();
    }

    public void Update(Order order)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Update(order);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var order = dbContext.Orders.Find(id);

        if (order == null) throw new KeyNotFoundException($"Order with {id.ToString()} id is Not found");
        ;

        dbContext.Orders.Remove(order);
        dbContext.SaveChanges();
    }
}