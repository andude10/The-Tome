using eBookShop.Data;
using eBookShop.Models;

namespace eBookShop.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext _dbContext;

        public BooksRepository()
        {
            _dbContext = new AppDbContext();
        }

        public Book? GetBook(int id)
        {
            return _dbContext.Books.Find(id);
        }
        public IEnumerable<Book> GetBooksFromOrder(Order order)
        {
            return order.Books;
        }
        public void Create(Book item) 
        {
            _dbContext.Add(item);
        } 
        public void Update(Book item)
        {
            _dbContext.Update(item);
        }
        public void Delete(int id)
        {
            var book = _dbContext.Books.Find(id);

            if(book != null)
            {
                _dbContext.Books.Remove(book);
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