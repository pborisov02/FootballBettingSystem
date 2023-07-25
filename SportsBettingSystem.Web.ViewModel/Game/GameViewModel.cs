using SportsBettingSystem.Web.ViewModels.League;
using SportsBettingSystem.Web.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Game
{
	public class GameViewModel
	{
		public Guid Id { get; set; }
		public decimal HomeOdd { get; set; }
		public decimal AwayOdd { get; set; }
		public decimal DrawOdd { get; set; }
		public int AwayTeamId { get; set; }
		public int HomeTeamId { get; set; }
		public TeamServiceModel HomeTeam { get; set; } = null!;
		public TeamServiceModel AwayTeam { get; set; } = null!;
		public LeagueServiceModel League { get; set; } = null!;
        public DateTime Start { get; set; }
		public string StartFormatted
		{
			get
			{
				return Start.ToString("HH:mm");
			}
		} 
	}
}
