namespace SportsBettingSystem.Web.ViewModels.Bank
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class WithdrawWalletFundsFormModel
	{
		[Required]
		[Display(Name = "Withdraw ammount")]
		public decimal WithdrawAmount { get; set; }
	}
}
