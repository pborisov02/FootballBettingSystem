using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Bank
{
	public class DepositWalletFundsFormModel
	{
		[Required]
		[Range(10, 1_000_000,ErrorMessage = "Ammount should be higher than 10$.")]
		[Display(Name = "Ammount to deposit")]
		public decimal DepositAmmount { get; set; }
	}
}
