using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; } 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
