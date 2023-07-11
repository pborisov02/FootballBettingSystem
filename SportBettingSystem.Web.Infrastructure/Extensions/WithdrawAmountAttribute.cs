namespace SportsBettingSystem.Web.Infrastructure.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class WithdrawAmountAttribute : ValidationAttribute
	{
		private readonly string _maxValueString;

		public WithdrawAmountAttribute(string maxValue)
		{
			_maxValueString = maxValue;
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (decimal.TryParse(_maxValueString, out decimal maxValue))
			{
				if (value is decimal withdrawAmount && (withdrawAmount < 0 || withdrawAmount > maxValue))
				{
					return new ValidationResult($"The withdraw amount must be between 0 and {maxValue}.");
				}
			}
			else
			{
				throw new InvalidDataException($"Invalid maximum value '{_maxValueString}' for WithdrawAmount attribute.");
			}

			return ValidationResult.Success!;
		}
	}
}
