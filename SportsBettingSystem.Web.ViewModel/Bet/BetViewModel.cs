using SportsBettingSystem.Web.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Bet
{
	public class BetViewModel
	{
		public Guid Id { get; set; }
		public virtual IEnumerable<Game_BetViewModel> Games { get; set; } = null!;
		public bool IsWinning { get; set; }
		public decimal Multiplier { get; set; }
		public decimal BetAmmount { get; set; }
	}
}
