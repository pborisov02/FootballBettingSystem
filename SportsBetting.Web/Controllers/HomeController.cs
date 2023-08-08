﻿namespace SportsBettingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Home;
    using System.Diagnostics;
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
	        if (User.IsInRole("Administrator"))
	        {
		        return RedirectToAction("Index", "Home", new { area = "Admin" });
	        }

	        return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}