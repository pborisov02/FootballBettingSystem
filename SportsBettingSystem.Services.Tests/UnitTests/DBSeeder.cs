#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SportsBettingSystem.Tests.UnitTests
{
	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;

	public class DBSeeder
	{
		public static ApplicationUser User;
		public static League League;
		public static Team Team;
		public static Game Game;
		public static Game FinishedGame;
		public static Bet Bet;

		public static void SeedDb(SportsBettingDbContext dbContext)
		{
			User = new ApplicationUser()
			{
				Id = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				UserName = "robi@robertocavalli.com",
				NormalizedUserName = "ROBI@ROBERTOCAVALLI.COM",
				Email = "robi@robertocavalli.com",
				NormalizedEmail = "ROBI@ROBERTOCAVALLI.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
				SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
				TwoFactorEnabled = false,
				FirstName = "Robi",
				LastName = "Cavalli",
				WalletBalance = 0
			};
			League = new League()
			{
				Country = "Bulgaria",
				Games = new List<Game>(),
				Id = 100,
				Name = "SoftBet",
				Teams = new List<Team>()
			};

			Team = new Team
			{
				Id = 100,
				Name = "Litex FC",
				LeagueId = 100,
				League = League,
				HomeGames = new List<Game>(),
				AwayGames = new List<Game>()
			};
			Game = new Game
			{
				Id = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				HomeTeamId = 100,
				HomeTeam = Team,
				AwayTeamId = 100,
				AwayTeam = Team,
				League = League,
				LeagueId = 100,
				Start = DateTime.Now,
				HomeOdd = 2,
				DrawOdd = 2,
				AwayOdd = 2,
				HomeGoals = 0,
				AwayGoals = 0,
				Result = 0,
				isFinished = false,
				GameBets = new List<GameBet>()
			};
			FinishedGame = new Game
			{
				Id = Guid.NewGuid(),
				HomeTeamId = 100,
				HomeTeam = Team,
				AwayTeamId = 100,
				AwayTeam = Team,
				League = League,
				LeagueId = 100,
				Start = default,
				HomeOdd = 2,
				DrawOdd = 2,
				AwayOdd = 2,
				HomeGoals = 2,
				AwayGoals = 1,
				Result = 1,
				isFinished = true,
				GameBets = null
			};
			Bet = new Bet
			{
				Id = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				GameBets = new List<GameBet>()
				{
					new GameBet()
					{
						GameId = FinishedGame.Id,
						Game = FinishedGame,
						BetId = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
						Bet = Bet,
						Prediction = 1
					}
				},
				IsWinning = false,
				Multiplier = 0,
				BetAmmount = 0,
				IsDone = false,
				UserId = Guid.Parse("19FC2C8C-2602-4DCB-F40D-08DB95AC10E3"),
				User = User
			};

			dbContext.Users.Add(User);
			dbContext.Leagues.Add(League);
			dbContext.Teams.Add(Team);
			dbContext.Games.Add(Game);
			dbContext.Bets.Add(Bet);
			dbContext.Games.Add(FinishedGame);

			dbContext.SaveChanges();
		}
	}
}
