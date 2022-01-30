using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Repositories.Implementations;
using TheTome.Repositories.Interfaces;
using TheTome.ViewModels;

namespace TheTome.Controllers;

public class CommunityController : Controller
{
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

    public IActionResult Feed()
    {
        var posts = _postsRepository.GetPosts();

        return View(new FeedViewModel(posts));
    }
}