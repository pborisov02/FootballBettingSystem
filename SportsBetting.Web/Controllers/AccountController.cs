using Microsoft.AspNetCore.Mvc;
using SportsBettingSystem.Services;
using SportsBettingSystem.Web.ViewModels.Account;
using Microsoft.Extensions.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SportsBettingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using SportsBettingSystem.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Griesoft.AspNetCore.ReCaptcha;

namespace SportsBettingSystem.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IAccountService accountService;
        public AccountController(IAccountService _accountService, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager)
        {
            this.accountService = _accountService;
            this.signInManager = _signInManager;
            this.userManager = _userManager;
        }
        public async Task<IActionResult> Info()
        {

            AccountViewModel accountViewModel = await accountService.AccountInfoAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View(accountViewModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        [AllowAnonymous]
        [ValidateRecaptcha(Action = nameof(Register),
            ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            await userManager.AddClaimAsync(user, new Claim("FirstName", model.FirstName));
           
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                this.ModelState
                    .AddModelError(string.Empty, "Unexpected error occurred while trying to log you into your account! Please try again later or contact administrator!");

                return View(model);
            }

            return Redirect(model.ReturnUrl ?? "/Home/Index");
        }
    }
}
