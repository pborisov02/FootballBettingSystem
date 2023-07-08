namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Team;
    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BadgeUrlMaxLength)]
        public string BadgeUrl { get; set; } = null!;


        public virtual ICollection<Player> Players { get; set; }

        [Required]
        [MaxLength(StadiumNameMaxLength)]
        public string StadiumName { get; set; } = null!;

        [Required]
        public int LeagueId { get; set; }


        [Required]
        public virtual League League { get; set; }


        public virtual ICollection<Game> HomeGames { get; set; }

        public virtual ICollection<Game> AwayGames { get; set; }



    }
}