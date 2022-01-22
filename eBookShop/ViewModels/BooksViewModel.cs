using eBookShop.Models;

namespace eBookShop.ViewModels;

public class BooksViewModel
{
    public BooksViewModel(IEnumerable<Book> books, IEnumerable<Book>? booksLikedByUser)
    {
        Books = books.ToList();

        if (booksLikedByUser != null) BooksLikedByUser = booksLikedByUser.ToList();
    }

    public List<Book>? BooksLikedByUser { get; set; }

    public List<Book> Books { get; set; }
}