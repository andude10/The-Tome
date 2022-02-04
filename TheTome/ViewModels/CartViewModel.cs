namespace TheTome.ViewModels;

public class CartViewModel
{
    public CartViewModel(BooksViewModel booksViewModel)
    {
        BooksViewModel = booksViewModel;
    }

    public double TotalPrice { get; set; }
    public int OrderId { get; set; }
    public BooksViewModel BooksViewModel { get; set; }
}