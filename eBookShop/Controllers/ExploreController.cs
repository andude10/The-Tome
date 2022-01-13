using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace eBookShop.Controllers
{
    public class ExploreController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public ExploreController()
        {
            _booksRepository = new BooksRepository();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}