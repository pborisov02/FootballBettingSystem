#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SportsBettingSystem.Tests.UnitTests
{
	using Microsoft.EntityFrameworkCore;
	using Services;
	using SportsBettingSystem.Data;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.League;
	using SportsBettingSystem.Web.ViewModels.Team;
	using static DBSeeder;
	public class TeamServiceTest
	{
		private DbContextOptions<SportsBettingDbContext> dbOptions;
		private SportsBettingDbContext dbContext;

		private ITeamService teamService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsBettingDbContext>()
				.UseInMemoryDatabase("SportsBettingInMemory" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsBettingDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDb(this.dbContext);

			this.teamService = new TeamService(this.dbContext);
		}

		[Test]
		public async Task CreateTeamShouldCreateTeam()
		{
			TeamFormModel team = new TeamFormModel()
			{
				LeagueId = 100,
				Leagues = new List<LeagueServiceModel>(),
				Name = "CSKA Sofia"
			};

			await teamService.CreateTeamAsync(team);

			Assert.True(dbContext.Teams.FirstOrDefault(t => t.Name == team.Name) != null);
		}

		[Test]
		public async Task LeagueExistsAsyncShouldReturnTrueWhenTeamExists()
		{
			//When seeding the db we create a league with id = 100, now let's check if it exists
			Assert.True(await teamService.LeagueExistsAsync(100));
		}

		[Test]
		public async Task LeagueExistsAsyncShouldReturnFalseWhenTeamDoesNotExists()
		{

			Assert.False(await teamService.LeagueExistsAsync(-1));
		}
	}
}
