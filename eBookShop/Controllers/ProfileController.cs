using eBookShop.Data;
using eBookShop.Repositories.Implementations;
using eBookShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

public class ProfileController : Controller
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    private readonly IUsersRepository _usersRepository;

    public ProfileController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _usersRepository = new UsersRepository(contextFactory);
        _contextFactory = contextFactory;
    }

    [Authorize]
    public IActionResult Info()
    {
        var user = _usersRepository.GetUser(User.Identity?.Name);

        _usersRepository.LoadOrders(user);
        _usersRepository.LoadLikedBooks(user);
        _usersRepository.LoadPosts(user);
        return View(user);
    }

    public IActionResult ShowOrders()
    {
        return View();
    }
}