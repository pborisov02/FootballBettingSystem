namespace SportsBettingSystem.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.League;

    public class League
    {
        public League()
        {
            this.Teams = new HashSet<Team>();
            this.Games= new HashSet<Game>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

		[Required]
        [MaxLength(CountryNameMaxLength)]
        public string Country { get; set; } = null!;

        public virtual ICollection<Team> Teams { get; set; }
		public virtual ICollection<Game> Games { get; set; }

	}
}
