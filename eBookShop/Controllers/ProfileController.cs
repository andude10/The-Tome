using eBookShop.Data;
using eBookShop.Models;
using eBookShop.Repositories;
using eBookShop.Repositories.Implementations;
using eBookShop.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBookShop.Controllers;

public class ProfileController : Controller
{
    private readonly IUsersRepository _usersRepository;
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    public ProfileController(IDbContextFactory<AppDbContext> contextFactory)
    {
        _usersRepository = new UsersRepository(contextFactory);
        _contextFactory = contextFactory;
    }

    [Authorize]
    public IActionResult Info()
    {
        var user = _usersRepository.GetUser(User.Identity.Name);

        if (user == null)
        {
            return NotFound(user);
        }
        
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