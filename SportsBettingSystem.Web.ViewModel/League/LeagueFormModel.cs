namespace SportsBettingSystem.Web.ViewModels.League
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.League;
    public class LeagueFormModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
		[MinLength(CountryNameMinLength)]
        [MaxLength(CountryNameMaxLength)]
		public string Country { get; set; } = null!;

    }
}
