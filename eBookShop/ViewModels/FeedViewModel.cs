using eBookShop.Models;

namespace eBookShop.ViewModels;

public class FeedViewModel
{
    public FeedViewModel(IEnumerable<Post> posts)
    {
        Posts = posts;
    }
    public IEnumerable<Post> Posts { get; set; }
}