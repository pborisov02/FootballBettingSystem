#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SportsBettingSystem.Services.Tests.UnitTests
{
	using Data;
	using Data.Models;
	public class DBSeeder
	{
		public static ApplicationUser User;

		public static League League;

		public static void SeedDb(SportsBettingDbContext dbContext)
		{
			User = new ApplicationUser()
			{
				Id = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				UserName = "robi@robertocavalli.com",
				NormalizedUserName = "ROBI@ROBERTOCAVALLI.COM",
				Email = "robi@robertocavalli.com",
				NormalizedEmail = "ROBI@ROBERTOCAVALLI.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
				SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
				TwoFactorEnabled = false,
				FirstName = "Robi",
				LastName = "Cavalli",
				WalletBalance = 0
			};
			League = new League()
			{
				Country = "Bulgaria",
				Games = new List<Game>(),
				Id = 100,
				Name = "SoftBet",
				Teams = new List<Team>()
			};

			dbContext.Users.Add(User);
			dbContext.Leagues.Add(League);

			dbContext.SaveChanges();
		}
	}
}
