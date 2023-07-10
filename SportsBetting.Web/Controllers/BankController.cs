using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsBettingSystem.Services;
using SportsBettingSystem.Services.Interfaces;
using SportsBettingSystem.Web.ViewModels.Bank;
using System.Security.Claims;

namespace SportsBettingSystem.Web.Controllers
{
	[Authorize]
	public class BankController : Controller
	{
		private readonly IBankService bankService;
		public BankController(IBankService bankService)
		{
			this.bankService = bankService;
		}

		[HttpGet]
		public async Task<IActionResult> Deposit()
		{
			DepositWalletFundsFormModel model = new DepositWalletFundsFormModel();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Deposit(DepositWalletFundsFormModel model)
		{
			if (!ModelState.IsValid)
			{
				return this.View(model);
			}
			try
			{
				await bankService.AddDepositAsync( User.FindFirstValue(ClaimTypes.NameIdentifier), model.DepositAmmount);
			}
			catch (Exception ex)
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Index", "Home");
		}
	}
}
