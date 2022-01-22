using eBookShop.Data;
using eBookShop.Repositories.Implementations;
using eBookShop.Repositories.Interfaces;
using eBookShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

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

        if (post == null) return NotFound(post);

        _postsRepository.LoadPostAuthor(post);

        return View(post);
    }

    public IActionResult Feed()
    {
        var posts = _postsRepository.GetPosts();

        return View(new FeedViewModel(posts));
    }
}