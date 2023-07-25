namespace SportsBettingSystem.Services.Interfaces
{
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Web.ViewModels.Bet;
	public interface IBetService
	{
		Task<OneGameBetServiceModel> CreateOneGameBetsAsync(string gameId, int prediction);
		Task<bool> CreateBetAsync(List<OneGameBetServiceModel> oneGameBets, decimal ammount, Guid userId);
		Task<IEnumerable<BetViewModel>> GetUserBetsAsync(Guid userId);
	}
}
