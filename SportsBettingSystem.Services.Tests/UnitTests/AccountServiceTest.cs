namespace SportsBettingSystem.Services.Tests.UnitTests
{
	using Microsoft.EntityFrameworkCore;
	using Data;
	using Interfaces;

	using static DBSeeder;
	public class AccountServiceTest
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private IAccountService accountService;
		
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsBettingDbContext>()
				.UseInMemoryDatabase("SportsBettingInMemory" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsBettingDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDb(this.dbContext);

			this.accountService = new AccountService(this.dbContext);
		}

		[Test]
		public async Task AccountInfoShouldReturnRightInfo()
		{
			var accountViewModel = await accountService.AccountInfoAsync(User.Id);

			bool areEqual = 
				accountViewModel.Name == $"{User.FirstName} {User.LastName}"
				&& accountViewModel.Email == User.Email
				&& accountViewModel.WalletBalance == User.WalletBalance;

			Assert.IsTrue(areEqual);
		}

		[Test]
		public void AccountInfoShouldThrowInvalidDataException()
		{
			Assert.ThrowsAsync<InvalidDataException>(() => 
				accountService.AccountInfoAsync(Guid.Parse("00000000-0000-0000-0000-000000000000")));
		}

		[Test]
	
		public void UpdateUserWalletBalanceShouldAddTheWinningsToTheUserWallet()
		{
			//When seeding the database we give the user wallet balance - 0$

			accountService.UpdateUserWalletBalance(User.Id, 50);

			Assert.True(User.WalletBalance == 50);
		}
	}
}
