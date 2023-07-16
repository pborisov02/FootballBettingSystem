using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsBettingSystem.Services.Interfaces;
using SportsBettingSystem.Web.ViewModels.League;

namespace SportsBettingSystem.Web.Controllers
{
	[Authorize]
    public class LeagueController : Controller
    {
		private readonly ILeagueService leagueService;
		public LeagueController(ILeagueService leagueService)
		{
			this.leagueService = leagueService;
		}
        [HttpGet]
        public IActionResult Add()
        {
            var leagueFormModel = new LeagueFormModel();

            return View(leagueFormModel);
        }

        [HttpPost]
		public async Task<IActionResult> Add(LeagueFormModel model)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}
			try
			{
				await leagueService.CreateAsync(model);
			}
			catch (Exception)
			{
				this.ModelState
					.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Info", "Account");
		}
	}
}
