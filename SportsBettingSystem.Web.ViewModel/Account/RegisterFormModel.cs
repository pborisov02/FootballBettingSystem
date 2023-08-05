using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SportsBettingSystem.Web.ViewModels.Account
{
	public class RegisterFormModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; } = null!;

		[Required]
		[MinLength(3)]
		[MaxLength(15)]
		public string FirstName { get; set; } = null!;

		[Required]
		[MinLength(3)]
		[MaxLength(15)]
		public string LastName { get; set; } = null!;
	}
}
