using eBookShop.Models;

namespace eBookShop.ViewModels;

public class CartViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public double TotalPrice { get; set; }
    public int OrderId { get; set; }
}