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
        Task CreateAsync(GameFormModel leagueFormModel);
		Task<IEnumerable<GameViewModel>> AllAsync();
        Task<IEnumerable<GameViewModel>> FilterByLeagueAndDate(int leagueId, DateTime date);
        Task<GamesForUpdateQueryModel> AllForChangesAsync(GamesForUpdateQueryModel queryModel);
        Task<GameUpdateServiceModel> GetGameForUpdateAsync(Guid gameId);
        Task<bool> UpdateGameAsync(GameUpdateServiceModel gameUpdate);
	}
    
}
