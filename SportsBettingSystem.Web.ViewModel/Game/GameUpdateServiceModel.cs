using SportsBettingSystem.Web.ViewModels.League;
using SportsBettingSystem.Web.ViewModels.Team;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SportsBettingSystem.Web.ViewModels.Game
{
    public class GameUpdateServiceModel
    {
        public Guid Id { get; set; }
        public TeamServiceModel HomeTeam { get; set; } = null!;
        public TeamServiceModel AwayTeam { get; set; } = null!;
        public string League { get; set; } = null!;
        public DateTime Start { get; set; }
        public bool IsFinished { get; set; }

        [Required]
		[Range(0, 99)]
		public int HomeGoals { get; set; }
		[Required]
        [Range(0, 99)]
		public int AwayGoals { get; set; }
        public int Result { get; set; }
    }
}
