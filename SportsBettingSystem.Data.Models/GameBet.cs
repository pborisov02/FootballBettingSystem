using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Data.Models
{
	public class GameBet
	{
		public Guid GameId { get; set; }
		public Game Game { get; set; } = null!;
		public Guid BetId { get; set; }
		public Bet Bet { get; set; } = null!;
		public int Prediction { get; set; }
		public bool IsWinning { get 
			{
				return isWinning();
			} }

		private bool isWinning()
		{
			if (this.Game.Result == this.Prediction)
				return true;
			else
				return false;
		}
	}
}
