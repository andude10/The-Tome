using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Repositories.Implementations;

public class PostsRepository : IPostsRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public PostsRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    /// <summary>
    ///     GetUser returns a post WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>Post WITHOUT associated data</returns>
    public Post? GetPost(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Posts.Find(id);
    }

    public IEnumerable<Post> GetPosts()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Posts.ToList();
    }

    public void LoadPostAuthor(Post post)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var id = post.Id;
        
        // To use the Load() method and load an object's associated data,
        // the object must be created in the current context
        var postInContext = dbContext.Posts.First(p => p.Id == id);
        dbContext.Entry(postInContext).Reference(p => p.User).Load();

        post.User = postInContext.User;
    }

    public void Create(Post item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Add(item);
        dbContext.SaveChanges();
    }

    public void Update(Post item)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Update(item);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var post = dbContext.Posts.Find(id);

        if (post == null) throw new KeyNotFoundException($"Post with {id.ToString()} id is Not found");

        dbContext.Posts.Remove(post);
        dbContext.SaveChanges();
    }
}