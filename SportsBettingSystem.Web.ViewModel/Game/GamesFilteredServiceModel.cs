using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Game
{
	public class GamesFilteredServiceModel
	{
		public GamesFilteredServiceModel()
		{
			this.Games = new HashSet<GameCardViewModel>();
		}
		public IEnumerable<GameCardViewModel> Games { get; set; }
	}
}
