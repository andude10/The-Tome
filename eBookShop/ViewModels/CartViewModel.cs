namespace eBookShop.ViewModels;

public class CartViewModel
{
    public double TotalPrice { get; set; }
    public int OrderId { get; set; }
    public BooksViewModel BooksViewModel { get; set; }
}