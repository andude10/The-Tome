using eBookShop.Models;

namespace eBookShop.ViewModels;

public class ExploreIndexViewModel
{
    public CatalogViewModel CatalogViewModel { get; set; }
    public PageViewModel PageViewModel { get; set; }
    public SortBookState SortBookState { get; set; }
}