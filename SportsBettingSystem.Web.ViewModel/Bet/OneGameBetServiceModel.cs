namespace SportsBettingSystem.Web.ViewModels.Bet
{
    using SportsBettingSystem.Web.ViewModels.Game;
    public class OneGameBetServiceModel
    {
        public GameViewModel Game { get; set; } = null!;
        public int Prediction { get; set; }
        public string PredictionString { get; set; } = null!;
		public decimal Multiplier { get; set; }
    }
}
