using eBookShop.Data;
using eBookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public BooksRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Book? GetBook(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Books.Find(id);
        }
        public void Create(Book item) 
        {
            using var context = _contextFactory.CreateDbContext();
            context.Add(item);
            context.SaveChanges();
        } 
        public void Update(Book item)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Update(item);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var book = context.Books.Find(id);

            if (book == null) return;
            
            context.Books.Remove(book);
            context.SaveChanges();
        }
    }
}