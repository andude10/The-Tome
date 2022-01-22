using eBookShop.Models;

namespace eBookShop.ViewModels;

public class BookViewerViewModel
{
    public BookViewerViewModel(bool isBought, Book book)
    {
        IsBought = isBought;
        Book = book;
    }

    public Book Book { get; set; }
    public bool IsBought { get; set; }
}