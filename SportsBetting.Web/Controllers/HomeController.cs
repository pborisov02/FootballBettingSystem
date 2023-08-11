namespace SportsBettingSystem.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
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
			if (statusCode == 400)
			{
				return View("Error400");
			}
			if (statusCode == 401)
			{
				return View("Error401");
			}

			return View();
		}
	}
}