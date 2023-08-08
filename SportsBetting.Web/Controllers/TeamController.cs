namespace SportsBettingSystem.Web.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.Team;

	[Authorize]
	public class TeamController : Controller
	{	
		private readonly ILeagueService _leagueService;
		private readonly ITeamService _teamService;
		public TeamController(ILeagueService leagueService,	ITeamService teamService)
		{ 
			this._leagueService= leagueService;
			this._teamService= teamService;
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			return View(new TeamFormModel()
			{
				Leagues = await _leagueService.AllLeaguesAsync()
			});
		}
		[HttpPost]
		public async Task<IActionResult> Add(TeamFormModel model)
		{
			if (!await _teamService.LeagueExistsAsync(model.LeagueId))
			{
				this.ModelState.AddModelError(nameof(model.LeagueId), "League does not exist");
			}

			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

			await _teamService.CreateTeamAsync(model);

			return this.RedirectToAction("Index", "Home");
		}
	}
}
