using eBookShop.Models;

namespace eBookShop.Repositories.Interfaces;

public interface IPostsRepository
{
    /// <summary>
    /// GetUser returns a post WITHOUT associated data. To load related data, you need to use the LoadList() methods
    /// </summary>
    /// <returns>Post WITHOUT associated data</returns>
    Post? GetPost(int id);

    IEnumerable<Post> GetPosts();

    void LoadPostAuthor(Post post);
    
    void Create(Post item);
    
    void Update(Post item);
    
    void Delete(int id);
}