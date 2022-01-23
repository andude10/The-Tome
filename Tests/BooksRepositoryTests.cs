using System;
using System.Collections.Generic;
using System.Linq;
using eBookShop.Models;
using eBookShop.Repositories.Implementations;
using Xunit;

namespace Tests;

public class BooksRepositoryTests
{
    private static Book GetTestBook()
    {
        return new Book
        {
            Id = 1,
            Title = "testTitle",
            CreatedDateTime = DateTime.Now,
            Description = "test Description test Description",
            Price = 12.5,
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
                    Id = 1,
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

    [Fact]
    public void Create_book()
    {
        var repository = new BooksRepository(new TestDbContextFactory());

        var book = GetTestBook();

        // Act
        repository.Create(book);

        var result = repository.GetBooks().ToList().Exists(b => b.Id == book.Id);
        Assert.True(result);
    }

    [Fact]
    public void Get_an_existing_book_by_Id()
    {
        var repository = new BooksRepository(new TestDbContextFactory());

        var book = GetTestBook();
        repository.Create(book);

        // Act
        var result = repository.GetBook(1);

        Assert.NotNull(result);
    }

    [Fact]
    public void Load_book_orders()
    {
        var repository = new BooksRepository(new TestDbContextFactory());

        var book = GetTestBook();
        repository.Create(book);

        var result = repository.GetBook(1);

        // Act
        repository.LoadBookOrders(result);

        Assert.NotEmpty(result.Orders);
    }

    [Fact]
    public void Load_users_who_like()
    {
        var repository = new BooksRepository(new TestDbContextFactory());

        var book = GetTestBook();
        repository.Create(book);

        var result = repository.GetBook(1);

        // Act
        repository.LoadUsersWhoLike(result);

        Assert.NotEmpty(result.UsersWhoLike);
    }

    [Fact]
    public void Load_book_categories()
    {
        var repository = new BooksRepository(new TestDbContextFactory());

        var book = GetTestBook();
        repository.Create(book);

        var result = repository.GetBook(1);

        // Act
        repository.LoadCategories(result);

        Assert.NotEmpty(result.Categories);
    }

    [Fact]
    public void Get_back_star_from_book()
    {
        var booksRepository = new BooksRepository(new TestDbContextFactory());

        var book = GetTestBook();
        booksRepository.Create(book);

        // Act
        // Test user has already liked
        booksRepository.GiveStarToBook(1, "testemail@gmail.com");
        book = booksRepository.GetBook(book.Id);

        Assert.StrictEqual(0, book.Stars);
    }
}