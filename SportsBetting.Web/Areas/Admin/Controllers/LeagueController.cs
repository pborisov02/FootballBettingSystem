namespace SportsBettingSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    using Services.Interfaces;
    using ViewModels.League;
	public class LeagueController : BaseAdminController
	{
		private readonly ILeagueService leagueService;
		public LeagueController(ILeagueService leagueService)
		{
			this.leagueService = leagueService;
		}
		[HttpGet]
		public IActionResult Create()
		{
			var leagueFormModel = new LeagueFormModel();

			return View(leagueFormModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(LeagueFormModel model)
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
					.AddModelError(string.Empty, "Unexpected error occurred while trying to add new league! Please try again later or contact administrator!");
				return this.View(model);
			}
			return this.RedirectToAction("Index", "Home", new { area = "Admin" });
		}
	}
}
