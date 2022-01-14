using eBookShop.Data;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eBookShop.Controllers
{
    public class ExploreController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public ExploreController(IDbContextFactory<AppDbContext> contextFactory)
        {
            _booksRepository = new BooksRepository(contextFactory);
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}