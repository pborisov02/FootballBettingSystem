namespace SportsBettingSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using ViewModels.Team;
    public class TeamController : BaseAdminController
    {
        private readonly ILeagueService _leagueService;
        private readonly ITeamService _teamService;
        public TeamController(ILeagueService leagueService, ITeamService teamService)
        {
            this._leagueService = leagueService;
            this._teamService = teamService;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new TeamFormModel()
            {
                Leagues = await _leagueService.AllLeaguesAsync()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamFormModel model)
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

            return this.RedirectToAction("Index", "Home", new {area = "Admin"});
        }
    }
}
