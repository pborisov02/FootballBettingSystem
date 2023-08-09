namespace SportsBettingSystem.Tests.IntegrationTests
{
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Microsoft.AspNetCore.Mvc;
	using Services;
	using Services.Interfaces;
	using Web.Controllers;
	using static UnitTests.DBSeeder;
	using SportsBettingSystem.Web.ViewModels.Bank;

	public class BankControllerTests
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private IBankService bankService;
		private IAccountService accountService;
		private BankController bankController;

		[OneTimeSetUp]
		public void Setup()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsBettingDbContext>()
				.UseInMemoryDatabase("SportsBettingInMemory" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsBettingDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDb(this.dbContext);

			this.bankService = new BankService(this.dbContext);
			this.accountService = new AccountService(this.dbContext);
			this.bankController = new BankController(this.bankService, this.accountService);
		}

		[Test]
		public void DepositShouldReturnView()
		{
			var result = this.bankController.Deposit();

			Assert.IsNotNull(result);

			var resultView = result as ViewResult;
			
			Assert.IsNotNull(resultView);
		}

		[Test]
		public void WithdrawShouldReturnView()
		{
			var result = this.bankController.Withdraw();

			Assert.IsNotNull(result);

			var resultView = result as ViewResult;

			Assert.IsNotNull(resultView);
		}
	}
}
