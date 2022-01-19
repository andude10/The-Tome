using eBookShop.Models;
using eBookShop.Repositories;

namespace eBookShop.ViewModels;

public class CatalogViewModel
{
    public CatalogViewModel(IEnumerable<Book> books, IEnumerable<Book> booksLikedByUser)
    {
        Books = books;
        BooksLikedByUser = booksLikedByUser;
    }
    
    public IEnumerable<Book> BooksLikedByUser { get; set; }

    public IEnumerable<Book> Books { get; set; }
}