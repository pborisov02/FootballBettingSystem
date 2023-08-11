namespace SportsBettingSystem.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Claims;
	
	using Services.Interfaces;
	using ViewModels.Bank;

	using static Common.NotificationMessagesConstants;
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

			TempData[SuccessMessage] = "Funds were successfully deposited!";
			return this.RedirectToAction("Info", "Account");
		}

		[HttpGet]
		public IActionResult Withdraw() 
		{
			WithdrawWalletFundsFormModel model = new();
			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Withdraw(WithdrawWalletFundsFormModel model)
		{
			Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var user = await accountService.AccountInfoAsync(userId);
			if (user.WalletBalance < model.WithdrawAmount || model.WithdrawAmount < 30)
			{
				this.ModelState
					.AddModelError(string.Empty, $"Cannot withdraw less than 30$ or more than your wallet balance({user.WalletBalance}$)!");
				return this.View(model);
			}
			try
			{
				await bankService.WithdrawAsync(userId, model.WithdrawAmount);
			}
			catch (Exception) 
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred! Please try again later or contact administrator!");
				return this.View(model);
			}
			TempData[SuccessMessage] = "Funds were successfully withdrawn!";
			return this.RedirectToAction("Info","Account");
		}
	}
}
