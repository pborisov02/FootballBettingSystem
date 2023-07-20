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
		Task<IEnumerable<GameCardViewModel>> AllAsync();
        Task<IEnumerable<GameCardViewModel>> FilterByLeagueAndDate(int leagueId, DateTime date);
	}
    
}
