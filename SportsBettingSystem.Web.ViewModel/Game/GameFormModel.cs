namespace SportsBettingSystem.Web.ViewModels.Game
{
    using System.ComponentModel.DataAnnotations;
    
    using SportsBettingSystem.Web.ViewModels.League;
    using SportsBettingSystem.Web.ViewModels.Team;
    using SportsBettingSystem.Web.Infrastructure.Extensions;

    public class GameFormModel
    {
        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        [Display(Name = "Date and time the match starts")]
        [CustomDateTimeAtribute()]
        public DateTime Start { get; set; }

        [Range(1.01, 10)]
        [Display(Name = "Home team to win multiplier")]
        public decimal HomeOdd { get; set; }
		[Range(1.01, 10)]
		[Display(Name = "Teams to draw multiplier")]
		public decimal DrawOdd { get; set; }
		[Range(1.01, 10)]
		[Display(Name = "Away team to win multiplier")]
		public decimal AwayOdd { get; set; }

        public ICollection<LeagueServiceModel>? Leagues { get; set; }
        public ICollection<TeamServiceModel>? Teams { get; set; }

    }
}
