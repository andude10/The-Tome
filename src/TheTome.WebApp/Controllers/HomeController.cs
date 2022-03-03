﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheTome.WebApp.ViewModels;

namespace TheTome.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}