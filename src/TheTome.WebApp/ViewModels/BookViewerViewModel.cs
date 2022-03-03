using TheTome.Core.Models;

namespace TheTome.WebApp.ViewModels;

public class BookViewerViewModel
{
    public BookViewerViewModel(bool isBought, bool isLiked, Book book)
    {
        IsBought = isBought;
        IsLiked = isLiked;
        Book = book;
    }

    public Book Book { get; set; }
    public bool IsBought { get; set; }
    public bool IsLiked { get; set; }
}