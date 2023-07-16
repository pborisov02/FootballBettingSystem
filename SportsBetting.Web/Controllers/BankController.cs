namespace SportsBettingSystem.Web.Controllers
{
	using System.Security.Claims;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.Bank;
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
			DepositWalletFundsFormModel model = new();
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
					.AddModelError(string.Empty, "Unexpected error occurred! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Info", "Account");
		}

		[HttpGet]
		public async Task<IActionResult> Withdraw() 
		{
			ApplicationUser user = await accountService.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
			WithdrawWalletFundsFormModel model = new WithdrawWalletFundsFormModel();
			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Withdraw(WithdrawWalletFundsFormModel model)
		{
			ApplicationUser user = await accountService.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if(user.WalletBallance < model.WithdrawAmount || model.WithdrawAmount < 30)
			{
				this.ModelState
					.AddModelError(string.Empty, $"Cannot withdraw less than 30$ or more than your wallet balance({user.WalletBallance}$)!");
				return this.View(model);
			}
			try
			{
				await bankService.WithdrawAsync(user.Id, model.WithdrawAmount);
			}
			catch (Exception) 
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Info","Account");
		}
	}
}
