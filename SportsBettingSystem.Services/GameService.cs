namespace SportsBettingSystem.Services
{
    using Microsoft.EntityFrameworkCore;
    using SportsBettingSystem.Data;
    using SportsBettingSystem.Data.Models;
    using SportsBettingSystem.Services.Interfaces;
    using SportsBettingSystem.Web.ViewModels.Game;
    using SportsBettingSystem.Web.ViewModels.League;
    using SportsBettingSystem.Web.ViewModels.Team;
    using System.Collections.Generic;

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

		public async Task<IEnumerable<GameCardViewModel>> GetGamesAsync(DateTime dateTime)
		{
            return await _db.Games
                .Where(g => g.Start.Date == dateTime.Date)
                .Select(g => new GameCardViewModel
                {
                    Id = g.Id,
                    HomeTeamId = g.HomeTeamId,
                    AwayTeamId = g.AwayTeamId,
                    HomeTeam = new TeamServiceModel 
                    {
                        Id = g.HomeTeam.Id,
                        Name = g.HomeTeam.Name,
                        LeagueId= g.HomeTeam.LeagueId,

                    },
                    AwayTeam = new TeamServiceModel 
                    {   Id = g.AwayTeam.Id,
                        Name = g.AwayTeam.Name,
                        LeagueId = g.AwayTeam.LeagueId
                    },
                    League = new LeagueServiceModel
                    {
                        Name = g.AwayTeam.League.Name,
                        Id = g.AwayTeam.League.Id
                    },
                    HomeOdd = g.HomeOdd,
					AwayOdd = g.AwayOdd,
                    DrawOdd = g.DrawOdd,
                    Start = g.Start
				})
                .ToListAsync();
		}
	}
}
