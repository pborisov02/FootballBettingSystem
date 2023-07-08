using Microsoft.AspNetCore.Mvc;

namespace SportsBettingSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Info()
        {
            return View();
        }
    }
}
