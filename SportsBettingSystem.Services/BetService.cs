namespace SportsBettingSystem.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SportsBettingSystem.Data;
    using SportsBettingSystem.Data.Models;
    using SportsBettingSystem.Services.Interfaces;
    using SportsBettingSystem.Web.ViewModels.Game;
    using SportsBettingSystem.Web.ViewModels.League;
    using SportsBettingSystem.Web.ViewModels.Team;
    using SportsBettingSystem.Web.ViewModels.Bet;
#pragma warning disable CS8602
#pragma warning disable CS8600
    public class BetService : IBetService
	{
		private readonly SportsBettingDbContext _db;
		private readonly IAccountService accountService;
		public BetService(SportsBettingDbContext dbContext, IAccountService accountService)
		{
			this._db = dbContext;
			this.accountService = accountService;
		}
		/// <summary>
		/// Creates a bet and saves it in the database
		/// </summary>
		/// <param name="oneGameBets"></param>
		/// <param name="amount"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<bool> CreateBetAsync(List<OneGameBetServiceModel> oneGameBets, decimal amount, Guid userId)
		{
			var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (oneGameBets.Count == 0 || oneGameBets!.Count < 1 || amount < 2 || user.WalletBalance < amount || user == null)
                return false;
			user.WalletBalance -= amount;
			decimal multiplier = 1;
            foreach (var game in oneGameBets)
			{
				multiplier *= game.Multiplier;
			}
			Bet bet = new()
			{
				BetAmmount = amount,
				Multiplier = multiplier,
				UserId = userId
			};
			bet.GameBets = oneGameBets.Select(og => new GameBet
			{
				BetId = bet.Id,
				GameId = og.Game.Id,
				Prediction = og.Prediction
				
			}).ToList();

			await _db.Bets.AddAsync(bet);
			await _db.SaveChangesAsync();
			return true;

		}
		/// <summary>
		/// Creates a one game bet, one bet can have many one game bets
		/// </summary>
		/// <param name="gameId"></param>
		/// <param name="prediction"></param>
		/// <returns></returns>
		public async Task<OneGameBetServiceModel> CreateOneGameBetsAsync(string gameId, int prediction)
		{
			decimal multiplier = 1;
			Game? game = await _db.Games.FirstAsync(g => g.Id == Guid.Parse(gameId));
			Team homeTeam = await _db.Teams.FindAsync(game.HomeTeamId);
			Team awayTeam = await _db.Teams.FindAsync(game.AwayTeamId);
			GameViewModel gameCardViewModel = new()
			{
				Id = game!.Id,
				HomeOdd = game.HomeOdd,
				DrawOdd = game.DrawOdd,
				AwayOdd = game.AwayOdd,
				HomeTeam = new TeamServiceModel()
				{
					Id = homeTeam.Id,
					Name = homeTeam.Name,
					LeagueId = homeTeam.LeagueId
				},
				AwayTeam = new TeamServiceModel()
				{
					Id = awayTeam.Id,
					Name = awayTeam.Name,
					LeagueId = awayTeam.LeagueId
				},
				Start = game.Start,
				League = new LeagueServiceModel()
			};

			if (prediction == 0)
				multiplier *= gameCardViewModel.DrawOdd;
			else if (prediction == 1)
				multiplier *= gameCardViewModel.HomeOdd;
			else if (prediction == 2)
				multiplier *= gameCardViewModel.AwayOdd;

			OneGameBetServiceModel oneGameBetService = new OneGameBetServiceModel()
			{
				Game = gameCardViewModel,
				Multiplier = multiplier,
				Prediction = prediction,

			};
			switch (prediction)
			{
				case 0: 
					oneGameBetService.PredictionString = "draw";
					break;
				case 1:
					oneGameBetService.PredictionString = "home";
					break;
				case 2:
					oneGameBetService.PredictionString = "away";
					break;
			}

			return oneGameBetService;
		}

		/// <summary>
		/// Returns a single bet that matches the bet Id and checks if the userId matches the bet.UserId
		/// </summary>
		/// <param name="betId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		/// <exception cref="AccessViolationException"></exception>
        public async Task<BetViewModel> GetUserBetAsync(Guid betId, Guid userId)
        {
			var bet = await _db.Bets.FindAsync(betId);
			if (bet.UserId != userId)
				throw new AccessViolationException();

			BetViewModel betViewModel = await _db.Bets
			.Include(b => b.GameBets)
			.ThenInclude(gb => gb.Game.HomeTeam)
            .Include(b => b.GameBets)
            .ThenInclude(gb => gb.Game.AwayTeam)
            .Where(b => b.Id == betId)
			.Select(b => new BetViewModel
			{
				Id = b.Id,
				IsWinning = b.IsWinning,
				Multiplier = b.Multiplier,
				BetAmmount = b.BetAmmount,
				Games = b.GameBets.Select(gb => new Game_BetViewModel
				{
					HomeTeam = new TeamServiceModel
					{
						Id = gb.Game.HomeTeam.Id,
						Name = gb.Game.HomeTeam.Name
					},
					AwayTeam = new TeamServiceModel
					{
						Id = gb.Game.AwayTeam.Id,
						Name = gb.Game.AwayTeam.Name
					},
					Start = gb.Game.Start,
					Prediction = gb.Prediction,
					HomeOdd = gb.Game.HomeOdd,
					DrawOdd = gb.Game.DrawOdd,
					AwayOdd = gb.Game.AwayOdd,
					isFinished = gb.Game.isFinished,
					Result = gb.Game.Result,
				})
			})
			.FirstOrDefaultAsync();

            return betViewModel!;
        }
		/// <summary>
		/// Returns every bet that the user with the given id has
		/// </summary>
		/// <param name="betId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		/// <exception cref="AccessViolationException"></exception>

		public async Task<IEnumerable<BetViewModel>> GetUserBetsAsync(Guid userId)
		{
			List<BetViewModel> bets =  await _db.Bets.Where(b => b.UserId == userId).Select( b => new BetViewModel
			{
				Id= b.Id,
				IsWinning = b.IsWinning,
				Multiplier= b.Multiplier,
				BetAmmount= b.BetAmmount,
				Games = b.GameBets.Select(gb => new Game_BetViewModel
				{
					HomeTeam = new TeamServiceModel
					{
						Id = gb.Game.HomeTeamId,
						Name = gb.Game.HomeTeam.Name
					},
					AwayTeam = new TeamServiceModel
					{
						Id = gb.Game.AwayTeamId,
						Name = gb.Game.AwayTeam.Name
					},
					Start = gb.Game.Start,
					Prediction = gb.Prediction, 
					HomeOdd = gb.Game.HomeOdd,
					DrawOdd= gb.Game.DrawOdd,
					AwayOdd= gb.Game.AwayOdd
				})
			}).ToListAsync();

			return bets;
		}

		/// <summary>
		/// Updates every bet that has a game matching the given gameId
		/// </summary>
		/// <param name="gameId"></param>
		/// <returns></returns>
		public async Task UpdateBetsAsync(Guid gameId)
		{
			var bets = await _db.Bets.Where(b => 
					b.GameBets.Any(gb => gb.GameId == gameId)
				&& b.GameBets.All(gb => gb.Game.isFinished) 
				&& !b.IsDone)
			.ToListAsync();
			if (bets != null)
			{
				foreach (var bet in bets)
				{
                    bet.IsDone = true;
					if (bet.GameBets.All(gb => gb.IsWinning))
					{
						bet.IsWinning = true;
						decimal winning = bet.Multiplier * bet.BetAmmount;
						await accountService.UpdateUserWalletBalance(bet.UserId, winning);
					}
					else
						bet.IsWinning = false;
				}
			}
		}
	}
}
