namespace SportsBettingSystem.Web.ViewModels.League
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.League;
    public class LeagueFormModel
    {
        [Required]
        [Range(NameMinLength,NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(CountryNameMinLength, CountryNameMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [Range(LogoUrlMinLength, LogoUrlMaxLength)]
        [Display(Name = "League photo link")]
        public string LogoUrl { get; set; } = null!;
    }
}
