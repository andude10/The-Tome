using eBookShop.Models;
using eBookShop.Repositories;

namespace eBookShop.ViewModels;

public class CatalogViewModel
{
    public CatalogViewModel(IEnumerable<Book> books, IEnumerable<Book> booksLikedByUser)
    {
        Books = books.ToList();
        BooksLikedByUser = booksLikedByUser.ToList();
    }
    
    public List<Book> BooksLikedByUser { get; set; }

    public List<Book> Books { get; set; }
}