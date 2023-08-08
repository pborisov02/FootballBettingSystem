using SportsBettingSystem.Web.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Services.Interfaces
{
    public interface IGameService
    {
        Task CreateGameAsync(GameFormModel leagueFormModel);
		Task<IEnumerable<GameViewModel>> AllGamesAsync();
        Task<IEnumerable<GameViewModel>> FilterByLeagueAndDateAsync(int leagueId, DateTime date);
        Task<GamesForUpdateQueryModel> AllGamesForChangesAsync(GamesForUpdateQueryModel queryModel);
        Task<GameUpdateServiceModel> GetGameForUpdateAsync(Guid gameId);
        Task<bool> UpdateGameAsync(GameUpdateServiceModel gameUpdate);
	}
    
}
