namespace SportsBettingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SportsBettingSystem.Data;
	using SportsBettingSystem.Services;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.Game;
    using SportsBettingSystem.Web.ViewModels.League;
    using SportsBettingSystem.Web.ViewModels.Team;

	public class GameController : Controller
	{
		private readonly ILeagueService leagueService;
		private readonly SportsBettingDbContext db;
		private readonly IGameService gameService;
		public GameController(SportsBettingDbContext dbContext, IGameService gameService, ILeagueService leagueService)
		{
			this.db = dbContext;
			this.gameService = gameService;
			this.leagueService = leagueService;
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			ViewData["Leagues"] = await leagueService.AllLeaguesAsync();
			ViewData["Teams"] = await db.Teams
				.Select(t => new TeamServiceModel
				{
					Id = t.Id,
					Name = t.Name
				}).ToListAsync();
			GameFormModel game = new()
			{
				Teams = new List<TeamServiceModel>(),
				Leagues = await db.Leagues.Select(l => new LeagueServiceModel
				{
					Id = l.Id,
					Name = l.Name
				}).ToListAsync(),
			};
			return View(game);
		}
		[HttpPost]
		public async Task<IActionResult> Add(GameFormModel model)
		{
			ViewData["Leagues"] = await db.Leagues.Select(l => new LeagueServiceModel
			{
				Id = l.Id,
				Name = l.Name
			}).ToListAsync();
			ViewData["Teams"] = await db.Teams
				.Select(t => new TeamServiceModel
				{
					Id = t.Id,
					Name = t.Name
				}).ToListAsync();
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}
			try
			{
				await gameService.CreateAsync(model);
			}
			catch (Exception)
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred while trying to add new game! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Index", "Home");
		}			

		[HttpGet]
		public async Task<IActionResult> GetTeamsForLeague(int leagueId)
		{
			var teams = await db.Teams
				.Where(t => t.LeagueId == leagueId)
				.Select(t => new
				{
					t.Id,
					t.Name
				})
				.ToListAsync();

			return Json(teams);
		}
		[HttpGet]
		public async Task<IActionResult> Show()
		{
			GameServiceModel model = new GameServiceModel()
			{
				Date = DateTime.Now,
				AllGames = await gameService.AllAsync(),
				Leagues = await leagueService.AllLeaguesAsync()
			};
			model.Games = await gameService.FilterByLeagueAndDate(-1 ,DateTime.UtcNow);
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> GetGamesByLeagueAndDate(int leagueId, double days)
		{
			var filteredGames = await gameService.FilterByLeagueAndDate(leagueId, DateTime.UtcNow.AddDays(days));
			return Json(filteredGames);
		}
	}
}
