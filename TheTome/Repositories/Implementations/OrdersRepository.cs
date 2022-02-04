using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Models;
using TheTome.Repositories.Interfaces;

namespace TheTome.Repositories.Implementations;

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
    public Order GetOrder(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var order = dbContext.Orders.First(o => o.Id == id);

        if (order == null)
        {
            throw new KeyNotFoundException($"No order found with id {id}");
        }
        
        Console.WriteLine($"DebugView: {dbContext.ChangeTracker.DebugView.ShortView}");
        return order;
    }

    /// <summary>
    ///     Loads all books in an order
    /// </summary>
    public void LoadBooks(Order order)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        dbContext.Entry(order).State = EntityState.Unchanged;
        dbContext.Entry(order).Collection(o => o.Books).Load();
        dbContext.Entry(order).State = EntityState.Detached;
    }

    public void AddBookToOrder(int orderId, int bookId, int userId)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var order = dbContext.Orders.First(o => o.Id == orderId);
        var book = dbContext.Books.First(b => b.Id == bookId);
        
        if (order == null)
        {
            throw new KeyNotFoundException($"No order found with id {orderId}");
        }
        
        if (order.IsCompleted)
        {
            var newOrder = new Order { UserId = userId, OrderDate = DateTime.Now };
            newOrder.Books.Add(book);

            Create(newOrder);
        }
        else
        {
            order.Books.Add(book);
            Update(order);
        }
    }
    
    public void RemoveBookFromOrder(int orderId, int bookId)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        
        var order = dbContext.Orders.Include(o => o.Books)
            .SingleOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            throw new KeyNotFoundException($"No order found with id {orderId}");
        }
        
        foreach (var book in order.Books.Where(b => b.Id == bookId).ToList())
        {
            order.Books.Remove(book);
        }
        
        dbContext.SaveChanges(); 
    }

    /// <summary>
    ///     Loads one book matching the given predicate
    /// </summary>
    public void LoadBook(Order order, Predicate<Book> predicate)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        order.Books= new List<Book>()
        {
            dbContext.Orders.Include(o => o.Books).AsTracking()
            .First(o => o.Id == order.Id)
            .Books.FirstOrDefault(b => predicate(b)) ?? throw new InvalidOperationException()
        };

        if (order.Books == null)
        {
            throw new KeyNotFoundException($"No book found with given predicate");
        }
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

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with {id.ToString()} id is Not found");
        }

        dbContext.Orders.Remove(order);
        dbContext.SaveChanges();
    }
}