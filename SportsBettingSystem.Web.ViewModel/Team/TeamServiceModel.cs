using SportsBettingSystem.Web.ViewModels.League;

namespace SportsBettingSystem.Web.ViewModels.Team
{
    public class TeamServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int LeagueId { get; set; }
    }
}
