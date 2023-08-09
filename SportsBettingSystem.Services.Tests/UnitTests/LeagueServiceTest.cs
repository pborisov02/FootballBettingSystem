namespace SportsBettingSystem.Tests.UnitTests
{
	using Microsoft.EntityFrameworkCore;
	using Services;
	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.League;
	using static DBSeeder;
	
	public class LeagueServiceTest
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private ILeagueService leagueService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsBettingDbContext>()
				.UseInMemoryDatabase("SportsBettingInMemory" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsBettingDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDb(this.dbContext);

			this.leagueService = new LeagueService(this.dbContext);
		}

		[Test]
		public void CreateLeagueShouldCreateALeagueAndAddItToTheDbContext()
		{
			LeagueFormModel leagueFormModel = new LeagueFormModel()
			{
				Name = "La liga",
				Country = "Spain"
			};

			leagueService.CreateAsync(leagueFormModel);

			League? league = dbContext.Leagues.FirstOrDefault(l => l.Name == "La liga");
			Assert.True(league != null);
		}

		[Test]
		public async Task AllLeaguesAsyncShouldReturnAllTheLeagues()
		{
			IEnumerable<LeagueServiceModel> leagues = await leagueService.AllLeaguesAsync();

			Assert.True(dbContext.Leagues.Count() == leagues.Count());
		}

		[Test]
		public async Task AllLeaguesNamesAsyncShouldReturnAllTheLeaguesNames()
		{
			IEnumerable<string> leagues = await leagueService.AllLeaguesNamesAsync();

			Assert.True(dbContext.Leagues.Count() == leagues.Count());
		}
	}
}
