namespace SportsBettingSystem.Web.ViewModels.Team
{
	using System.ComponentModel.DataAnnotations;

	using SportsBettingSystem.Web.ViewModels.League;
	using static Common.EntityValidationConstants.Team;

	public class TeamFormModel
	{
		[Required]
		[MinLength(NameMinLength)]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MinLength(BadgeUrlMinLength)]
		[MaxLength(BadgeUrlMaxLength)]
		public string BadgeUrl { get; set; } = null!;

		[Required]
		[MinLength(StadiumNameMinLength)]
		[MaxLength(StadiumNameMaxLength)]
		[Display(Name = "Stadium Name")]
		public string StadiumName { get; set; } = null!;

		[Required]
		[Display(Name = "League")]
		public int LeagueId { get; set; }


		public IEnumerable<LeagueServiceModel>? Leagues { get; set; }
	}
}
