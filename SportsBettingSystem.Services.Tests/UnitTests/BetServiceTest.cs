#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SportsBettingSystem.Services.Tests.UnitTests
{
	using Microsoft.EntityFrameworkCore;
	using Data;
	using Interfaces;
	using Web.ViewModels.Bet;
	using Web.ViewModels.Game;
	using static DBSeeder;

	public class BetServiceTest
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private IAccountService accountService;
		private IBetService betService;

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
			this.betService = new BetService(this.dbContext, accountService);
		}

		[Test]
		public async Task CreateBetAsyncShouldReturnTrueAndCreateAGame()
		{
			List<OneGameBetServiceModel> betServiceModels = new List<OneGameBetServiceModel>()
			{
				new OneGameBetServiceModel
				{
					Game = new GameViewModel()
					{
						Id = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3")
					},
					Prediction = 1,
					PredictionString = "home",
					Multiplier = 2.33m
				},
			};
			User.WalletBalance = 20;
			bool isOk = await betService.CreateBetAsync(
				betServiceModels,
				10,
				Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"));

			Assert.True(isOk &&
			            dbContext.Bets.Any(b => b.UserId == Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3")));
		}

		[Test]
		public async Task CreateBetAsyncShouldNotReturnTrueAndCreateAGame()
		{
			List<OneGameBetServiceModel> betServiceModels = new List<OneGameBetServiceModel>()
			{
				new OneGameBetServiceModel
				{
					Game = new GameViewModel()
					{
						Id = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3")
					},
					Prediction = 1,
					PredictionString = "home",
					Multiplier = 2.33m
				},
			};
			User.WalletBalance = 20;
			bool isOk = await betService.CreateBetAsync(
				betServiceModels,
				0,
				Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"));

			Assert.False(
				isOk && dbContext.Bets.Any(b => b.UserId == Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3")));
		}

		[Test]
		public async Task CreateOneGameBetsAsyncShouldCreateOneGameBet()
		{
			OneGameBetServiceModel oneGameBet = await betService.CreateOneGameBetsAsync(DBSeeder.Game.Id.ToString(), 2);

			Assert.True(oneGameBet.Game.Id == Game.Id);
		}

		[Test]
		public async Task GetUserBetAsyncShouldReturnBet()
		{
			BetViewModel betViewModel = await betService.GetUserBetAsync(
				Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"));

			Assert.NotNull(betViewModel);
		}
		[Test]
		public async Task GetUserBetAsyncShouldThrow()
		{
			Assert.ThrowsAsync<AccessViolationException>(()=> betService.GetUserBetAsync(
				Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				Guid.Parse("00000000-0000-0000-0000-000000000000")));

			
		}

		[Test]
		public async Task GetUserBetsAsyncShouldReturnTheUsersBets()
		{
			IEnumerable<BetViewModel> bets = await betService.GetUserBetsAsync(Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"));

			Assert.True(bets.Count() > 0);
		}
		[Test]
		public async Task GetUserBetsAsyncShouldReturnEmptyCollection()
		{
			IEnumerable<BetViewModel> bets = await betService.GetUserBetsAsync(Guid.Parse("00000000-0000-0000-0000-000000000000"));

			Assert.True(bets.Count() == 0);
		}

		[Test]
		public async Task UpdateBetAsyncShouldUpdateBet()
		{
			await betService.UpdateBetsAsync(FinishedGame.Id);

			Assert.True(Bet.IsWinning);
		}
	}
}
