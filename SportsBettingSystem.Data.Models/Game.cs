namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        public Game()
        {
            this.Id = Guid.NewGuid();
            this.GameBets = new HashSet<GameBet>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(HomeTeam))]
        public int HomeTeamId { get; set; }

        [Required]
        public Team HomeTeam { get; set; } = null!;


        [Required]
        [ForeignKey(nameof(AwayTeam))]
        public int AwayTeamId { get; set; }

        [Required]
        public Team AwayTeam { get; set; } = null!;

        [Required]
        public League League { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public decimal HomeOdd { get; set; }

        public decimal DrawOdd { get; set; }
        public decimal AwayOdd { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int Result { get; set; }
        public bool isFinished { get; set; }
        public virtual ICollection<GameBet> GameBets { get; set; }
    }
}
