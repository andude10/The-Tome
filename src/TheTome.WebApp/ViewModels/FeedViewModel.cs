using TheTome.Core.Models;

namespace TheTome.WebApp.ViewModels;

public class FeedViewModel
{
    public FeedViewModel(PostsViewModel postsViewModel, PageViewModel pageViewModel, SortPostState sortPostState)
    {
        PostsViewModel = postsViewModel;
        PageViewModel = pageViewModel;
        SortPostState = sortPostState;
    }

    public PageViewModel PageViewModel { get; set; }
    public PostsViewModel PostsViewModel { get; set; }
    public SortPostState SortPostState { get; set; }
}