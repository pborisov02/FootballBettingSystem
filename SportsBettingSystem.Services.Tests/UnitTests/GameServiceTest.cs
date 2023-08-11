namespace SportsBettingSystem.Tests.UnitTests
{
	using Microsoft.EntityFrameworkCore;
	using Services;
	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.Game;
	using SportsBettingSystem.Web.ViewModels.League;
	using SportsBettingSystem.Web.ViewModels.Team;
	using static DBSeeder;
	public class GameServiceTest
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private IGameService gameService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsBettingDbContext>()
				.UseInMemoryDatabase("SportsBettingInMemory" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsBettingDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDb(this.dbContext);

			this.gameService = new GameService(this.dbContext);
		}

		[Test]
		public void CreateGameAsyncShouldCreateGameAndAddItToTheDbContext()
		{
			GameFormModel gameFormModel = new GameFormModel
			{
				HomeTeamId = 100,
				AwayTeamId = 100,
				LeagueId = 100,
				Start = DateTime.Now,
				HomeOdd = 2,
				DrawOdd = 3,
				AwayOdd = 1,
				Leagues = new List<LeagueServiceModel>(),
				Teams = new List<TeamServiceModel>()
			};

			gameService.CreateGameAsync(gameFormModel);

			Assert.True(dbContext.Games.Count()! > 1);
		}

		[Test]
		public async Task AllGamesAsyncShouldReturnAllGames()
		{
			IEnumerable<GameViewModel> games = await gameService.AllGamesAsync();

			Assert.True(games.Count() == dbContext.Games.Where(g => !g.isFinished).Count());
		}
		[Test]
		public async Task FilterByLeagueAndDateShouldReturnFilteredGamesCollection()
		{
			IEnumerable<GameViewModel> gamesFilteredAutomatically = await gameService.FilterByLeagueAndDateAsync(100, DateTime.Now);

			List<Game> gamesFilteredManually = dbContext.Games.Where(g => g.LeagueId == 100 && g.Start.Date == DateTime.Now.Date).ToList();

			Assert.True(gamesFilteredAutomatically.Count() == gamesFilteredManually.Count());
		}
		[Test]
		public async Task FilterByLeagueAndDateShouldReturnEmptyCollection()
		{
			IEnumerable<GameViewModel> gamesFilteredAutomatically = await gameService.FilterByLeagueAndDateAsync(-1, DateTime.MinValue);

			List<Game> gamesFilteredManually = dbContext.Games.Where(g => g.LeagueId == -1 && g.Start.Date == DateTime.MinValue.Date).ToList();

			Assert.True(gamesFilteredAutomatically.Count() == gamesFilteredManually.Count());
		}

		[Test]
		public async Task GetGameForUpdateAsyncShouldReturnRightGame()
		{
			//In the db seeder we are creating a game with id = GUID("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3")

			var gameForUpdate =
				await gameService.GetGameForUpdateAsync(Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"));

			Assert.True(gameForUpdate.Id == DBSeeder.Game.Id);
		}
		[Test]
		public void GetGameForUpdateAsyncShouldThrowInvalidDataExceptionWhenGivenIdThatDoesNotExistInTheDb()
		{

			Assert.ThrowsAsync<InvalidDataException>(() =>
				gameService.GetGameForUpdateAsync(Guid.Parse("00000000-0000-0000-0000-000000000000")));
		}

		[Test]
		public async Task UpdateGameAsyncShouldReturnTrue()
		{
			var gameForUpdate =
				await gameService.GetGameForUpdateAsync(Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"));

			Assert.True(await gameService.UpdateGameAsync(gameForUpdate));
		}
		[Test]
		public async Task UpdateGameAsyncShouldReturnFalse()
		{
			GameUpdateServiceModel gameForUpdate = new GameUpdateServiceModel
			{
				Id = Guid.Empty,
				HomeTeam = null,
				AwayTeam = null,
				League = null,
				Start = default,
				IsFinished = false,
				HomeGoals = -1,
				AwayGoals = -2,
				Result = 0
			};

			Assert.False(await gameService.UpdateGameAsync(gameForUpdate));
		}
		[Test]
		public async Task AllGamesForChangesAsyncShouldReturnEveryGameWithGivenLeagueForFilter()
		{
			GamesForUpdateQueryModel gamesForUpdateQueryModel = new GamesForUpdateQueryModel
			{
				League = "SoftBet",
			};

			gamesForUpdateQueryModel = await gameService.AllGamesForChangesAsync(gamesForUpdateQueryModel);

			Assert.True(gamesForUpdateQueryModel.GamesForUpdate.Count() == dbContext.Games.Where(g => g.League.Name == "SoftBet" && !g.isFinished).ToList().Count());
		}
		[Test]
		public async Task AllGamesForChangesAsyncShouldReturnEveryGameWithGivenSearchTermForFilter()
		{
			GamesForUpdateQueryModel gamesForUpdateQueryModel = new GamesForUpdateQueryModel
			{
				SearchString = "Litex FC"
			};

			gamesForUpdateQueryModel = await gameService.AllGamesForChangesAsync(gamesForUpdateQueryModel);

			Assert.True(gamesForUpdateQueryModel.GamesForUpdate.Count() == dbContext.Games.Where(g => (g.HomeTeam.Name == "Litex FC" || g.AwayTeam.Name == "Litex FC") && !g.isFinished).ToList().Count());
		}
		[Test]
		public async Task AllGamesForChangesAsyncShouldReturnEveryGameWithGivenDatesForFilter()
		{
			GamesForUpdateQueryModel gamesForUpdateQueryModel = new GamesForUpdateQueryModel
			{
				From = DBSeeder.Game.Start.AddDays(-1),
				To = DBSeeder.Game.Start.AddDays(1)
			};

			gamesForUpdateQueryModel = await gameService.AllGamesForChangesAsync(gamesForUpdateQueryModel);

			List<Game> gamesToCheck = dbContext.Games
				.Where(g => g.Start.Date <= DBSeeder.Game.Start.AddDays(1) && g.Start.Date >= DBSeeder.Game.Start.AddDays(-1))
				.ToList();
			Assert.True(gamesForUpdateQueryModel.GamesForUpdate.Count() == gamesToCheck.Count());
		}
		[Test]
		public async Task AllGamesForChangesAsyncShouldReturnEveryGameWithGivenFromDateForFilter()
		{
			GamesForUpdateQueryModel gamesForUpdateQueryModel = new GamesForUpdateQueryModel
			{
				From = DBSeeder.Game.Start.AddDays(-1),
			};

			gamesForUpdateQueryModel = await gameService.AllGamesForChangesAsync(gamesForUpdateQueryModel);

			List<Game> gamesToCheck = dbContext.Games
				.Where(g => g.Start.Date >= DBSeeder.Game.Start.AddDays(-1))
				.ToList();
			Assert.True(gamesForUpdateQueryModel.GamesForUpdate.Count() == gamesToCheck.Count());
		}
		[Test]
		public async Task AllGamesForChangesAsyncShouldReturnEveryGameWithGivenToDateForFilter()
		{
			GamesForUpdateQueryModel gamesForUpdateQueryModel = new GamesForUpdateQueryModel
			{
				To = DBSeeder.Game.Start.AddDays(1)
			};

			gamesForUpdateQueryModel = await gameService.AllGamesForChangesAsync(gamesForUpdateQueryModel);

			List<Game> gamesToCheck = dbContext.Games
				.Where(g => g.Start.Date <= DBSeeder.Game.Start.AddDays(1) && !g.isFinished)
				.ToList();
			Assert.True(gamesForUpdateQueryModel.GamesForUpdate.Count() == gamesToCheck.Count());
		}
	}
}
