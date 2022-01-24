using eBookShop.Models;

namespace eBookShop.ViewModels;

public class BooksViewModel
{
    public BooksViewModel(IEnumerable<Book> books, IEnumerable<Book>? booksLikedByUser, IEnumerable<Book>? booksInCart)
    {
        Books = books.ToList();
        if (booksInCart != null) BooksInCart = booksInCart.ToList();
        if (booksLikedByUser != null) BooksLikedByUser = booksLikedByUser.ToList();
    }

    public List<Book>? BooksLikedByUser { get; }
    public List<Book>? BooksInCart { get; }

    public List<Book> Books { get; }
}