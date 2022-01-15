using eBookShop.Models;

namespace eBookShop.ViewModels;

public class ExploreIndexViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public PageViewModel PageViewModel { get; set; }
    public SortBookState SortBookState { get; set; }
}