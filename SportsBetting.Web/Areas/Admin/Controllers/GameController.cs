namespace SportsBettingSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Services.Interfaces;
    using ViewModels.Game;
    using ViewModels.League;
    using ViewModels.Team;
    public class GameController : BaseAdminController
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
        public async Task<IActionResult> Create()
        {
            ViewData["Leagues"] = await leagueService.AllLeaguesAsync();
            ViewData["Teams"] =  await db.Teams
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
        public async Task<IActionResult> Create(GameFormModel model)
        {
            if(model.HomeTeamId == model.AwayTeamId)
                this.ModelState.AddModelError(nameof(model.AwayTeamId), "A team cannot play itself");
            if (!this.ModelState.IsValid)
            {
				ViewData["Leagues"] = await leagueService.AllLeaguesAsync();
				ViewData["Teams"] = await db.Teams
					.Select(t => new TeamServiceModel
					{
						Id = t.Id,
						Name = t.Name
					}).ToListAsync();

				model.Leagues = await db.Leagues.Select(l => new LeagueServiceModel
	            {
		            Id = l.Id,
		            Name = l.Name
	            }).ToListAsync();

				return this.View(model);
            }
            try
            {
                await gameService.CreateGameAsync(model);
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
        public async Task<IActionResult> ShowGamesForUpdate([FromQuery] GamesForUpdateQueryModel queryModel, int currentPage)
        {
            queryModel.CurrentPage = currentPage;
            queryModel = await gameService.AllGamesForChangesAsync(queryModel);
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
                if (await gameService.UpdateGameAsync(gameForUpdate))
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

        public async Task<IActionResult> Delete(Guid gameId)
        {
	        await gameService.DeleteGame(gameId);
	        return RedirectToAction("Index", "Home");
        }
    }
}