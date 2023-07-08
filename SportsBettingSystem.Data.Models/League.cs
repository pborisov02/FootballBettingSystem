namespace SportsBettingSystem.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.League;

    public class League
    {
        public League()
        {
            this.Teams = new HashSet<Team>()  ;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(LogoUrlMaxLength)]
        public string LogoUrl { get; set; } = null!;

        public virtual ICollection<Team> Teams { get; set; }

    }
}
