namespace SportsBettingSystem.Services.Interfaces
{
	using SportsBettingSystem.Web.ViewModels.Team;

	public interface ITeamService
	{
		Task CreateTeam(TeamFormModel model);
		Task<bool> LeagueExists(int leagueId);
	}
}
