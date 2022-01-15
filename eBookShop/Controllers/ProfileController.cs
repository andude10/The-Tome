using eBookShop.Data;
using eBookShop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

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
        var user = _usersRepository.GetUser(User.Identity.Name);

        if (user != null) return View(user);

        return NotFound();
    }

    public IActionResult ShowOrders()
    {
        return View();
    }
}