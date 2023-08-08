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
        public async Task CreateGameAsync(GameFormModel gameFormModel)
        {
			await _db.Games.AddAsync(new Game
			{
				HomeTeamId = gameFormModel.HomeTeamId,
				AwayTeamId = gameFormModel.AwayTeamId,
				Start = gameFormModel.Start,
				AwayOdd = gameFormModel.AwayOdd,
				HomeOdd = gameFormModel.HomeOdd,
				DrawOdd = gameFormModel.DrawOdd,
				LeagueId = gameFormModel.LeagueId
			});

            await _db.SaveChangesAsync();
        }

		public async Task<IEnumerable<GameViewModel>> AllGamesAsync()
		{

            IEnumerable<GameViewModel> games = await _db.Games
				.Where(g => !g.isFinished)
				.Select(g => new GameViewModel
                {
					Id = g.Id,
					HomeTeam = new TeamServiceModel
					{
						Id = g.HomeTeam.Id,
						Name = g.HomeTeam.Name,
						LeagueId = g.HomeTeam.LeagueId,

					},
					AwayTeam = new TeamServiceModel
					{
						Id = g.AwayTeam.Id,
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
                .OrderBy(g => g.League.Name)
                .ToArrayAsync();


            return games;
		}

		public async Task<IEnumerable<GameViewModel>> FilterByLeagueAndDateAsync(int leagueId, DateTime date)
		{
			IEnumerable<GameViewModel> games = await AllGamesAsync();
			if (leagueId == -1)
			{
				games = games.Where(g => g.Start.Date == date.Date).ToList();
				return games;
			}
			
			games = games.Where(g => g.Start.Date == date.Date).Where(g => g.League.Id == leagueId).ToList();
			return games;
		}

        public async Task<GamesForUpdateQueryModel> AllGamesForChangesAsync(GamesForUpdateQueryModel queryModel)
        {
			var gamesQuery = _db.Games.AsQueryable();

			if(!String.IsNullOrWhiteSpace(queryModel.League))
			{
				gamesQuery = gamesQuery
					.Where(g => g.HomeTeam.League.Name == queryModel.League);
			}
			if(!String.IsNullOrWhiteSpace(queryModel.SearchString))
			{
				gamesQuery = gamesQuery
					.Where(g => 
					g.HomeTeam.Name.ToLower().Contains(queryModel.SearchString.ToLower())
					|| g.AwayTeam.Name.ToLower().Contains(queryModel.SearchString.ToLower()));
			}
			if(queryModel.From.HasValue && !queryModel.To.HasValue) 
			{
				gamesQuery = gamesQuery.Where(g => g.Start.Date >= queryModel.From.Value.Date);
			}
			if(queryModel.To.HasValue && !queryModel.From.HasValue)
			{
				gamesQuery = gamesQuery.Where(g => g.Start.Date <= queryModel.To.Value.Date);
			}
			if(queryModel.To.HasValue && queryModel.From.HasValue)
			{
				gamesQuery = gamesQuery.Where(g => 
				g.Start.Date <= queryModel.To.Value.Date
				&& g.Start.Date >= queryModel.From.Value.Date);
			}

			if(queryModel.CurrentPage < 1)
				queryModel.CurrentPage = 1;

			queryModel.GamesForUpdate = await gamesQuery
				.Skip((queryModel.CurrentPage - 1) *10)
				.Take(10)
				.Select(g => new GameUpdateServiceModel
				{
					Id = g.Id,
					HomeTeam = new TeamServiceModel
					{
						Id = g.HomeTeam.Id,
						Name = g.HomeTeam.Name
					},
                    AwayTeam = new TeamServiceModel
                    {
                        Id = g.AwayTeam.Id,
                        Name = g.AwayTeam.Name
                    },
					Start = g.Start
                })
				.ToListAsync();
			queryModel.GamesCount = gamesQuery.Count();

			return queryModel;
        }

		public async Task<GameUpdateServiceModel> GetGameForUpdateAsync(Guid gameId)
		{
			Game? game = await _db.Games.Include(g => g.HomeTeam).Include(g=>g.AwayTeam).Include(g => g.League).FirstOrDefaultAsync(g => g.Id == gameId);
			if (game == null)
				throw new InvalidDataException();
			GameUpdateServiceModel gameForUpdate = new()
			{
				Id = game.Id,
				HomeTeam = new TeamServiceModel
				{
					Name = game.HomeTeam.Name,
					Id = game.HomeTeam.Id
				},
				AwayTeam = new TeamServiceModel
				{
					Name = game.AwayTeam.Name,
					Id = game.AwayTeam.Id
				},
				Start = game.Start,
				League = game.League.Name
			};

			return gameForUpdate;
		}

		public async Task<bool> UpdateGameAsync(GameUpdateServiceModel gameUpdate)
		{
			if (gameUpdate.Id != Guid.Empty && gameUpdate.HomeGoals >= 0 && gameUpdate.AwayGoals >= 0)
			{
				Game game = await _db.Games.FirstAsync(g => g.Id == gameUpdate.Id);
				game.AwayGoals = gameUpdate.AwayGoals;
				game.HomeGoals = gameUpdate.HomeGoals;
				if (game.HomeGoals > game.AwayGoals)
					game.Result = 1;
				else if (game.HomeGoals < game.AwayGoals)
					game.Result = 2;
				else if(game.HomeGoals == game.AwayGoals)
					game.Result = 0;
				game.isFinished = true;
				await _db.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
