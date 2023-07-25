namespace SportsBettingSystem.Web.ViewModels.Game
{
    using SportsBettingSystem.Web.ViewModels.Bet;
    using SportsBettingSystem.Web.ViewModels.League;
    public class GameServiceModel
	{
		public GameServiceModel()
		{
			Leagues = new List<LeagueServiceModel>();
			Games = new List<GameViewModel>();
			OneGameBets = new List<OneGameBetServiceModel>();
			DaysToAdd = 0;
			LeagueId = -1;
		}
		public DateTime Date { get; set; }
		public double DaysToAdd { get; set; }
		public LeagueServiceModel? League { get; set; }
		public int LeagueId { get; set; }
		public IEnumerable<LeagueServiceModel>? Leagues { get; set; }
		public IEnumerable<GameViewModel>? Games { get; set; }
		public IEnumerable<GameViewModel>? AllGames { get; set; }
		public IEnumerable<OneGameBetServiceModel> OneGameBets { get; set; }
	}
}
