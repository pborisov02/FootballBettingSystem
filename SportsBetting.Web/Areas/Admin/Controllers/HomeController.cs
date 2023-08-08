using Microsoft.AspNetCore.Mvc;

namespace SportsBettingSystem.Web.Areas.Admin.Controllers
{
	public class HomeController : BaseAdminController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
