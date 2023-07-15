using SportsBettingSystem.Web.ViewModels.League;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Services.Interfaces
{
	public interface ILeagueService
	{
		Task CreateAsync(LeagueFormModel leagueFormModel);
		Task<IEnumerable<LeagueServiceModel>> AllLeagues();
	}
}
