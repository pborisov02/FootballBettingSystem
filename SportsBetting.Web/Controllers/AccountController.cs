using Microsoft.AspNetCore.Mvc;
using SportsBettingSystem.Services;
using SportsBettingSystem.Web.ViewModels.Account;
using Microsoft.Extensions.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SportsBettingSystem.Services.Interfaces;

namespace SportsBettingSystem.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public async Task<IActionResult> Info()
        {

            AccountViewModel accountViewModel = await accountService.DisplayAccountInfo(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(accountViewModel);
        }
    }
}
