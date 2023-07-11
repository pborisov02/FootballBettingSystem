using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsBettingSystem.Data.Models;
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
		private readonly IAccountService accountService;
		public BankController(IBankService bankService, IAccountService accountService)
		{
			this.bankService = bankService;
			this.accountService = accountService;
		}

		[HttpGet]
		public IActionResult Deposit()
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
			catch (Exception)
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Info", "Account");
		}

		[HttpGet]
		public async Task<IActionResult> Withdraw() 
		{
			ApplicationUser user = await accountService.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
			WithdrawWalletFundsFormModel model = new WithdrawWalletFundsFormModel(user.WalletBallance);
			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Withdraw(WithdrawWalletFundsFormModel model)
		{
			ApplicationUser user = await accountService.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
			try
			{
				await bankService.WithdrawAsync(user.Id, model.WithdrawAmount);
			}
			catch (Exception ex) 
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Info","Account");
		}
	}
}
