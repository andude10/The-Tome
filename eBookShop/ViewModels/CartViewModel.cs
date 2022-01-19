using eBookShop.Models;

namespace eBookShop.ViewModels;

public class CartViewModel
{
    public double TotalPrice { get; set; }
    public int OrderId { get; set; }
    public CatalogViewModel CatalogViewModel { get; set; }
}