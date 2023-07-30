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

		public async Task<bool> CreateBetAsync(List<OneGameBetServiceModel> oneGameBets, decimal ammount, Guid userId)
		{
			var user = await _db.Users.FirstAsync(u => u.Id == userId);
            if (oneGameBets == null || oneGameBets!.Count < 1 || ammount < 2 || user.WalletBallance < ammount)
                return false;
			user.WalletBallance -= ammount;
			decimal multiplier = 1;
            foreach (var game in oneGameBets)
			{
				multiplier *= game.Multiplier;
			}
			Bet bet = new()
			{
				BetAmmount = ammount,
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

			return oneGameBetService;
		}

        public async Task<BetViewModel> GetUserBetAsync(Guid betId)
        {
			Bet b = await _db.Bets.FindAsync(betId);

			BetViewModel betViewModel = new()
			{
				Id = b.Id,
				IsWinning = b.IsWinning,
				Multiplier = b.Multiplier,
				BetAmmount = b.BetAmmount,
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
					DrawOdd = gb.Game.DrawOdd,
					AwayOdd = gb.Game.AwayOdd
				})
			};
			return betViewModel;
        }

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

		public async Task UpdateBetsAsync(Guid gameId)
		{
			var bets = await _db.Bets.Where(b => b.GameBets.Any(gb => gb.GameId == gameId)
			&& b.GameBets.All(gb => gb.Game.isFinished) 
			&& !b.IsDone)
			.ToListAsync();
			if (bets != null)
			{
				foreach (var bet in bets)
				{
					if(bet.GameBets.All(gb => gb.IsWinning))
					{
						bet.IsDone = true;
						bet.IsWinning = true;
						decimal winning = bet.Multiplier * bet.BetAmmount;
						await accountService.UpdateUserWallet(bet.UserId, winning);
					}
				}
			}
		}
	}
}
