using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Core.Models;
using TheTome.Core.RepositoriesInterfaces;
using TheTome.Infrastructure.Data;
using TheTome.Infrastructure.RepositoriesImplementations;
using TheTome.WebApp.ViewModels;

namespace TheTome.WebApp.Controllers;

public class CommunityController : Controller
{
    private const int PageSize = 18;
    private readonly IPostsRepository _postsRepository;

    public CommunityController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _postsRepository = new PostsRepository(contextFactory);
    }

    public IActionResult PostViewer(int postId)
    {
        var post = _postsRepository.GetPost(postId);

        _postsRepository.LoadPostAuthor(post);

        return View(post);
    }

    public IActionResult Feed(int pageId = 1, SortPostState sortPostState = SortPostState.New)
    {
        var source = _postsRepository.GetPosts(pageId, PageSize, sortPostState).ToList();

        return View(new FeedViewModel(
            new PostsViewModel(source),
            new PageViewModel(source.Count, pageId, PageSize),
            sortPostState
        ));
    }
}