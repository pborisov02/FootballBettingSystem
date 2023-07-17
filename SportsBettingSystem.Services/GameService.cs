namespace SportsBettingSystem.Services
{
    using SportsBettingSystem.Data;
    using SportsBettingSystem.Data.Models;
    using SportsBettingSystem.Services.Interfaces;
    using SportsBettingSystem.Web.ViewModels.Game;
    using SportsBettingSystem.Web.ViewModels.League;

    public class GameService : IGameService
    {
        private readonly SportsBettingDbContext _db;
        public GameService(SportsBettingDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(GameFormModel gameFormModel)
        {
            await _db.Games.AddAsync(new Game
            {
                HomeTeamId = gameFormModel.HomeTeamId,
                AwayTeamId = gameFormModel.AwayTeamId,
                Start = DateTime.UtcNow,
                AwayOdd = gameFormModel.AwayOdd,
                HomeOdd = gameFormModel.HomeOdd,
                DrawOdd= gameFormModel.DrawOdd
            });

            await _db.SaveChangesAsync();
        }
    }
}
