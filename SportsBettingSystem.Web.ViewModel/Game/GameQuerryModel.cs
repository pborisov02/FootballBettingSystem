namespace SportsBettingSystem.Web.ViewModels.Game
{
	using SportsBettingSystem.Web.ViewModels.League;
	public class GameQuerryModel
	{
		public GameQuerryModel()
		{
			Leagues = new List<LeagueServiceModel>();
			Games = new List<GameCardViewModel>();
			DaysToAdd = 0;
			LeagueId = -1;
		}
		public DateTime Date { get; set; }
		public double DaysToAdd { get; set; }
		public LeagueServiceModel? League { get; set; }
		public int LeagueId { get; set; }
		public IEnumerable<LeagueServiceModel>? Leagues { get; set; }
		public IEnumerable<GameCardViewModel>? Games { get; set; }
		public IEnumerable<GameCardViewModel>? AllGames { get; set; }
	}
}
