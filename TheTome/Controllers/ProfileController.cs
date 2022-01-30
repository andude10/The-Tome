using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Data;
using TheTome.Repositories.Implementations;
using TheTome.Repositories.Interfaces;

namespace TheTome.Controllers;

public class ProfileController : Controller
{
    private readonly IUsersRepository _usersRepository;

    public ProfileController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _usersRepository = new UsersRepository(contextFactory);
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