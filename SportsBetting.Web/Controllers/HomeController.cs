namespace SportsBettingSystem.Web.Controllers
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
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 400 || statusCode == 404)
			{
				return View();
			}

			if (statusCode == 401)
			{
				return View();
			}

			return View();
		}
	}
}