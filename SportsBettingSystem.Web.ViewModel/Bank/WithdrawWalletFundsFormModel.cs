namespace SportsBettingSystem.Web.ViewModels.Bank
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class WithdrawWalletFundsFormModel : IValidatableObject
	{
		private readonly decimal _max;
		public WithdrawWalletFundsFormModel(decimal max)
		{
			_max = max;
		}

		public decimal WithdrawAmount { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (WithdrawAmount < 0 || WithdrawAmount > _max)
			{
				yield return new ValidationResult($"The withdraw amount must be between 0 and {_max}.", new[] { nameof(WithdrawAmount) });
			}
		}
	}
}
