using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace eBookShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public CatalogController(AppDbContext db)
        {
            _booksRepository = new BooksRepository(db);
        }

        public IActionResult Index()
        {
            return View(_booksRepository.GetBooks());
        }
    }
}