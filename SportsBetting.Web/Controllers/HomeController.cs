﻿namespace SportsBettingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SportsBettingSystem.Web.ViewModels.Home;
    using System.Diagnostics;
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}