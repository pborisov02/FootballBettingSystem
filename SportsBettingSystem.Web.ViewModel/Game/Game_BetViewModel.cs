using SportsBettingSystem.Web.ViewModels.League;
using SportsBettingSystem.Web.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Game
{
	public class Game_BetViewModel
	{
		public Guid Id { get; set; }
		public decimal HomeOdd { get; set; }
		public decimal AwayOdd { get; set; }
		public decimal DrawOdd { get; set; }
		public TeamServiceModel HomeTeam { get; set; } = null!;
		public TeamServiceModel AwayTeam { get; set; } = null!;
		public LeagueServiceModel League { get; set; } = null!;
		public int Prediction { get; set; }
		public decimal PredictionOdd { get 
			{
				return GetPredictionOdd();
			} }
		public DateTime Start { get; set; }
		public string StartFormatted
		{
			get
			{
				return Start.ToString("HH:mm");
			}
		}

		private decimal GetPredictionOdd()
		{
			if (this.Prediction == 0)
				return this.DrawOdd;
			else if(this.Prediction == 1)
				return this.HomeOdd;
			else
				return this.AwayOdd;
		}
	}
}
