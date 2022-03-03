using TheTome.Core.Models;

namespace TheTome.WebApp.ViewModels;

public class PostsViewModel
{
    public PostsViewModel(IEnumerable<Post> posts)
    {
        Posts = posts;
    }

    public IEnumerable<Post> Posts { get; set; }
}