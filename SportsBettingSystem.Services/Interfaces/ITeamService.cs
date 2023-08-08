namespace SportsBettingSystem.Services.Interfaces
{
	using SportsBettingSystem.Web.ViewModels.Team;

	public interface ITeamService
	{
		Task CreateTeamAsync(TeamFormModel model);
		Task<bool> LeagueExistsAsync(int leagueId);
	}
}
