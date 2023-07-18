namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        public Game()
        {
            this.Id = Guid.NewGuid();
            this.Bets = new HashSet<Bet>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Title { get { return $"{HomeTeam.Name} - {AwayTeam.Name}"; } }

        [Required]
        [ForeignKey(nameof(HomeTeam))]
        public int HomeTeamId { get; set; }
        
        [Required]
        public Team HomeTeam { get; set; }


        [Required]
        [ForeignKey(nameof(AwayTeam))]
        public int AwayTeamId { get; set; }

        [Required]
        public Team AwayTeam { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public decimal HomeOdd { get; set; }

        public decimal DrawOdd { get; set; }
        public decimal AwayOdd { get; set; }

        public int Result { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
