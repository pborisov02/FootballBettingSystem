﻿namespace SportsBettingSystem.Services.Interfaces
{
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Web.ViewModels.Bet;
	public interface IBetService
	{
		Task<OneGameBetServiceModel> CreateOneGameBetsAsync(string gameId, int prediction);
		Task<bool> CreateBetAsync(List<OneGameBetServiceModel> oneGameBets, decimal amount, Guid userId);
		Task<IEnumerable<BetViewModel>> GetUserBetsAsync(Guid userId);
		Task<BetViewModel> GetUserBetAsync(Guid betId, Guid userId);
		Task UpdateBetsAsync(Guid gameId);
	}
}
