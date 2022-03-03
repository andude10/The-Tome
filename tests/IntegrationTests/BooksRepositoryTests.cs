using System;
using System.Collections.Generic;
using System.Linq;
using TheTome.Core.Models;
using TheTome.Core.RepositoriesInterfaces;
using TheTome.Infrastructure.RepositoriesImplementations;
using Xunit;

namespace IntegrationTests;

// TODO: Some tests fail when run all at once, but work when run individually
// Until the reason is clear, I think that each test uses its own instance of the BookRepository
// (respectively, its own instance of the database context)
public class BooksRepositoryTests
{
    private static Book GetTestBook()
    {
        return new Book
        {
            Title = "testTitle",
            CreatedDateTime = DateTime.Now,
            Description = "test Description test Description",
            Price = 5.5,
            Author = "testAuthor",
            CoverUrl = new Uri("https://i.ibb.co/cLxQCzc/Thomas-Malory.jpg"),
            Stars = 1,
            Orders = new List<Order>
            {
                new() {UserId = 1}
            },
            UsersWhoLike = new List<User>
            {
                new()
                {
                    Name = "testName",
                    AboutUser = "about user about user",
                    Email = "testemail@gmail.com",
                    Password = "testPass"
                }
            },
            Categories = new List<Category>
            {
                new()
                {
                    Name = "testCategory"
                }
            }
        };
    }

    private static IBooksRepository CreateDefaultRepository()
    {
        return new BooksRepository(new TestDbContextFactory());
    }

    [Fact]
    public void Create_book()
    {
        // Arrange
        var repository = CreateDefaultRepository();
        var book = GetTestBook();

        // Act
        repository.Create(book);

        var result = repository.GetBooks(0, 5, SortBookState.Popular)
            .ToList()
            .Exists(b => b.Id == book.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Get_an_existing_book_by_Id()
    {
        // Arrange
        var repository = CreateDefaultRepository();
        repository.Create(GetTestBook());

        // Act
        var result = repository.GetBook(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Load_book_orders()
    {
        // Arrange
        var repository = CreateDefaultRepository();
        repository.Create(GetTestBook());

        var result = repository.GetBook(1);

        // Act
        repository.LoadBookOrders(result);

        // Assert
        Assert.NotEmpty(result.Orders);
    }

    [Fact]
    public void Load_users_who_like()
    {
        // Arrange
        var repository = CreateDefaultRepository();
        repository.Create(GetTestBook());

        var result = repository.GetBook(1);

        // Act
        repository.LoadUsersWhoLike(result);

        // Assert
        Assert.NotEmpty(result.UsersWhoLike);
    }

    [Fact]
    public void Load_book_categories()
    {
        // Arrange
        var repository = CreateDefaultRepository();
        repository.Create(GetTestBook());

        var result = repository.GetBook(1);

        // Act
        repository.LoadCategories(result);

        // Assert
        Assert.NotEmpty(result.Categories);
    }

    [Fact]
    public void Get_back_star_from_book()
    {
        // Arrange
        var repository = CreateDefaultRepository();
        var book = GetTestBook();
        repository.Create(book);

        // Act
        // Test user has already liked
        repository.GiveStarToBook(1, "testemail@gmail.com");
        book = repository.GetBook(book.Id);

        // Assert
        Assert.StrictEqual(0, book.Stars);
    }
}