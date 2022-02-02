using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Models;
using TheTome.Repositories.Interfaces;

namespace TheTome.Repositories.Implementations;

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
    public Post GetPost(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var post = dbContext.Posts.Find(id);

        if (post == null) throw new KeyNotFoundException($"No post found with id {id}");

        return post;
    }

    /// <summary>
    ///     Returns a list of posts with no associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Post> GetPosts(int skipSize, int takeSize, SortPostState sortPostState)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var result = dbContext.Posts.Skip(skipSize)
            .Take(takeSize)
            .ToList();

        return SortPosts(sortPostState, result);
    }

    public void LoadPostAuthor(Post post)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        dbContext.Entry(post).State = EntityState.Unchanged;
        dbContext.Entry(post).Reference(p => p.User).Load();
        dbContext.Entry(post).State = EntityState.Detached;
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

    public IEnumerable<Post> GetPosts()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Posts.ToList();
    }

    /// <summary>
    ///     The SortPosts returns a sorted list of posts.
    /// </summary>
    /// <param name="posts">The posts</param>
    /// <param name="sortPostState">Sort state</param>
    /// <returns></returns>
    private IEnumerable<Post> SortPosts(SortPostState sortPostState, IEnumerable<Post> posts)
    {
        return sortPostState switch
        {
            SortPostState.New => posts.OrderBy(p => p.Date),
            SortPostState.TodayBest => posts.Where(p => p.Date.Day == DateTime.Now.Day)
                .OrderBy(p => p.Rating),
            SortPostState.YearBest => posts.Where(p => p.Date.Year == DateTime.Now.Year)
                .OrderBy(p => p.Rating),
            SortPostState.MonthBest => posts.Where(p => p.Date.Month == DateTime.Now.Month)
                .OrderBy(p => p.Rating),
            _ => posts
        };
    }
}