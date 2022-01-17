using eBookShop.Models;

namespace eBookShop.ViewModels;

public class BookViewerViewModel
{
    public BookViewerViewModel(User user, Book book)
    {
        User = user;
        Book = book;
    }
    public User User { get; set; }
    public Book Book { get; set; }
    public bool IsBought => User.Orders.Last().Books.Exists(b => b.Id == Book.Id);
}