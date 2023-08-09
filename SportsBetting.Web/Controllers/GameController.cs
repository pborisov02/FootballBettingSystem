namespace SportsBettingSystem.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	
	using Data;
	using Services.Interfaces;
	using ViewModels.Game;

    public class GameController : Controller
    {
        private readonly ILeagueService leagueService;
        private readonly IGameService gameService;

        public GameController(IGameService gameService, ILeagueService leagueService)
        {
            this.gameService = gameService;
            this.leagueService = leagueService;

        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            GameServiceModel model = new()
            {
                Date = DateTime.Now,
                AllGames = await gameService.AllGamesAsync(),
                Leagues = await leagueService.AllLeaguesAsync(),
                Games = await gameService.FilterByLeagueAndDateAsync(-1, DateTime.UtcNow)
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGamesByLeagueAndDate(int leagueId, double days)
        {
            var filteredGames = await gameService.FilterByLeagueAndDateAsync(leagueId, DateTime.UtcNow.AddDays(days));
            return Json(filteredGames);
        }
    }
}
