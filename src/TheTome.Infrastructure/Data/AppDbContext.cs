using Microsoft.EntityFrameworkCore;
using TheTome.Core.Models;

namespace TheTome.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
}