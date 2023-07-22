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
		public int Prediction { get; set; }
		public bool isWinning { get { return IsWon(this.Game); } }	
		public bool GameIsFinished { get 
			{
				return this.Game.isFinished;
			} }
		public Guid BetId { get; set; }
		public Bet Bet { get; set; } = null!;

		private bool IsWon (Game game)
		{
			if (game.isFinished || this.Prediction == game.Result)
				return true;
			else 
				return false;
		}
	}
}
