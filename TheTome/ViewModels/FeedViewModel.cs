namespace TheTome.ViewModels;

public class FeedViewModel
{
    public FeedViewModel(PostsViewModel postsViewModel)
    {
        PostsViewModel = postsViewModel;
    }

    public PostsViewModel PostsViewModel { get; set; }
}