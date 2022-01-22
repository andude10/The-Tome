using eBookShop.Models;

namespace eBookShop.ViewModels;

public class CatalogViewModel
{
    public BooksViewModel BooksViewModel { get; set; }
    public PageViewModel PageViewModel { get; set; }
    public SortBookState SortBookState { get; set; }
}