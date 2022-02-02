using TheTome.Models;

namespace TheTome.ViewModels;

public class PostsViewModel
{
    public PostsViewModel(IEnumerable<Post> posts)
    {
        Posts = posts;
    }

    public IEnumerable<Post> Posts { get; set; }
}