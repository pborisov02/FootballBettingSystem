namespace SportsBettingSystem.Web.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using Services.Interfaces;
    using ViewModels.Bet;

	[Authorize]
	public class BetController : Controller
	{
		private readonly IBetService betService;
		public BetController(IBetService _betService)
		{
			this.betService = _betService;
		}
		[HttpGet]
		public async Task<IActionResult> CreateOneGameBet(string gameId, int prediction)
		{
			OneGameBetServiceModel gameBet = await betService.CreateOneGameBetsAsync(gameId, prediction);
			return Json(gameBet);
		}
		[HttpPost]
		public async Task<IActionResult> CreateBet(decimal betAmount, List<OneGameBetServiceModel> oneGameBets)
		{
            Console.WriteLine(User.FindFirstValue(ClaimTypes.NameIdentifier));
			Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (await betService.CreateBetAsync(oneGameBets, betAmount, userId))
				return RedirectToAction("UserBets", "Bet");
			else
				return Json(0);

		}
		public async Task<IActionResult> UserBets()
		{
			Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			IEnumerable<BetViewModel> bets = await betService.GetUserBetsAsync(userId);
			return View(bets);

		}

		public async Task<IActionResult> ShowSelectedBet(Guid betId)
		{
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			try
			{
				var betViewModel = await betService.GetUserBetAsync(betId, userId);
                return View(betViewModel);
            }
			catch (Exception)
			{
				return View("Error");
			}
		}
	}
}
