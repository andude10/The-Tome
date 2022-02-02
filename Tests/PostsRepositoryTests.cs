using System.Linq;
using TheTome.Models;
using TheTome.Repositories.Implementations;
using TheTome.Repositories.Interfaces;
using Xunit;

namespace Tests;

public class PostsRepositoryTests
{
    private static Post GetTestPost()
    {
        return new Post
        {
            Title = "Test post title",
            HtmlContent = "<p>test Paragraph test Paragraph test Paragraph</p>",
            Rating = 15,
            User = new User
            {
                Name = "testName",
                AboutUser = "about user about user",
                Email = "testemail@gmail.com",
                Password = "testPass"
            }
        };
    }

    private static IPostsRepository CreateDefaultRepository()
    {
        return new PostsRepository(new TestDbContextFactory());
    }

    [Fact]
    public void Create_post()
    {
        var repository = CreateDefaultRepository();

        var post = GetTestPost();

        // Act
        repository.Create(post);

        var result = repository.GetPosts(0, 5, SortPostState.New)
            .ToList()
            .Exists(b => b.Id == post.Id);
        Assert.True(result);
    }

    [Fact]
    public void Get_an_existing_post_by_Id()
    {
        var repository = CreateDefaultRepository();

        repository.Create(GetTestPost());

        // Act
        var result = repository.GetPost(1);

        Assert.NotNull(result);
    }

    [Fact]
    public void Load_post_author()
    {
        var repository = CreateDefaultRepository();

        repository.Create(GetTestPost());

        var result = repository.GetPost(1);

        // Act
        repository.LoadPostAuthor(result);

        Assert.NotNull(result.User);
    }
}