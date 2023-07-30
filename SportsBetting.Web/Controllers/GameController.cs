namespace SportsBettingSystem.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	
	using SportsBettingSystem.Data;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.Game;
	using SportsBettingSystem.Web.ViewModels.League;
	using SportsBettingSystem.Web.ViewModels.Team;

	public class GameController : Controller
	{
		private readonly ILeagueService leagueService;
		private readonly SportsBettingDbContext db;
		private readonly IGameService gameService;
		private readonly IBetService betService;
		public GameController(SportsBettingDbContext dbContext, IGameService gameService, ILeagueService leagueService, IBetService betService)
		{
			this.db = dbContext;
			this.gameService = gameService;
			this.leagueService = leagueService;
			this.betService = betService;
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
            GameServiceModel model = new()
            {
                Date = DateTime.Now,
                AllGames = await gameService.AllAsync(),
                Leagues = await leagueService.AllLeaguesAsync(),
                Games = await gameService.FilterByLeagueAndDate(-1, DateTime.UtcNow)
            };
            return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> GetGamesByLeagueAndDate(int leagueId, double days)
		{
			var filteredGames = await gameService.FilterByLeagueAndDate(leagueId, DateTime.UtcNow.AddDays(days));
			return Json(filteredGames);
		}

		public async Task<IActionResult> ShowGamesForUpdate([FromQuery] GamesForUpdateQueryModel queryModel, int currentPage)
		{
			queryModel.CurrentPage = currentPage;
			queryModel = await gameService.AllForChangesAsync(queryModel);
			queryModel.Leagues = await leagueService.AllLeaguesNamesAsync();
			return View(queryModel);
		}

		[HttpGet]
		public async Task<IActionResult> UpdateGame(Guid gameId)
		{
			GameUpdateServiceModel gameForUpdate = await gameService.GetGameForUpdateAsync(gameId);
			return View(gameForUpdate);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateGame(GameUpdateServiceModel gameForUpdate)
		{
			try
			{
				if(await gameService.UpdateGameAsync(gameForUpdate))
				{
					await betService.UpdateBetsAsync(gameForUpdate.Id);
				}
			}
			catch (Exception)
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred while trying to update game! Please try again later or contact administrator!");
				return this.View(gameForUpdate);

			}
			return RedirectToAction("ShowGamesForUpdate", "Game");
		}

	}
}
