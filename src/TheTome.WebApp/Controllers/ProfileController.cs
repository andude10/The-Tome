using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheTome.Core.RepositoriesInterfaces;
using TheTome.Infrastructure.Data;
using TheTome.Infrastructure.RepositoriesImplementations;

namespace TheTome.WebApp.Controllers;

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
        var user = _usersRepository.GetUser(User.Identity?.Name ?? throw new InvalidOperationException());

        _usersRepository.LoadOrders(user);
        _usersRepository.LoadLikedBooks(user);
        _usersRepository.LoadPosts(user);

        return View(user);
    }
}