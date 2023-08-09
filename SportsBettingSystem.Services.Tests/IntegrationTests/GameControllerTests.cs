namespace SportsBettingSystem.Tests.IntegrationTests
{
	using Microsoft.EntityFrameworkCore;
	
	using Data;
	using Microsoft.AspNetCore.Mvc;
	using Services;
	using Services.Interfaces;
	using Web.Controllers;
	using static UnitTests.DBSeeder;
	public class GameControllerTests
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private ILeagueService leagueService;
		private IGameService gameService;
		private GameController gameController;

		[OneTimeSetUp]
		public void Setup()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsBettingDbContext>()
				.UseInMemoryDatabase("SportsBettingInMemory" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsBettingDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDb(this.dbContext);
			
			this.leagueService = new LeagueService(this.dbContext);
			this.gameService = new GameService(this.dbContext);
			this.gameController = new GameController(this.gameService, this.leagueService);
		}

		[Test]
		public async Task GetGamesByLeagueShouldReturnJson()
		{
			var result = await gameController.GetGamesByLeagueAndDate(100,1);

			Assert.IsNotNull(result);

			var resultJson = result as JsonResult;
			
			Assert.IsNotNull(resultJson);
		}

		[Test]
		public async Task ShowShouldReturnView()
		{
			var result = await gameController.Show();

			Assert.IsNotNull(result);
			
			var resultView = result as ViewResult;
			
			Assert.IsNotNull(resultView);
		}
	}
}
