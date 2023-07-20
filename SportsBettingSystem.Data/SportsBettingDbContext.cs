﻿namespace SportsBettingSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SportsBettingSystem.Data.Models;
    using System.Reflection.Emit;

    public class SportsBettingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SportsBettingDbContext(DbContextOptions<SportsBettingDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<League> Leagues { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Bet> Bets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.Entity<GameBet>().HasKey(gb => new { gb.GameId, gb.BetId});

			builder.Entity<GameBet>()
				.HasOne(gb => gb.Game)
				.WithMany(g => g.GameBets)
				.HasForeignKey(gb => gb.GameId).OnDelete(DeleteBehavior.Restrict);
            


			builder.Entity<GameBet>()
				.HasOne(gb => gb.Bet)
				.WithMany(g => g.GameBets)
				.HasForeignKey(gb => gb.BetId)
                .OnDelete(DeleteBehavior.Restrict);



			builder.Entity<Team>()
                .HasOne(t => t.League)
                .WithMany(l => l.Teams)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasMany(t => t.HomeGames)
                .WithOne(g => g.HomeTeam)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasMany(t => t.AwayGames)
                .WithOne(g => g.AwayTeam)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
                
                
                builder.Entity<League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.League)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .Property(u => u.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);


            builder.Entity<League>().HasData(GenerateLeagues());
            builder.Entity<Team>().HasData(GenerateTeams());
            base.OnModelCreating(builder);

        }

        private static League[] GenerateLeagues()
        {
            List<League> leagues = new();
            var league = new League
            {
                Id = 1,
                Name = "La Liga",
                Country = "Spain",
                LogoUrl = "https://content.sportslogos.net/leagues/thumbs/130.gif",
            };
            leagues.Add(league);
            league = new League
            {
                Id = 2,
                Name = "Bundesliga",
                Country = "Germany",
                LogoUrl = "https://content.sportslogos.net/leagues/thumbs/132.gif",
            };
            leagues.Add(league);

            return leagues.ToArray();
        }

        private static Team[] GenerateTeams()
        {
            var teams = new List<Team>();
            var team = new Team
            {
                Id = 1,
                Name = "Barcelona",
                BadgeUrl = "https://content.sportslogos.net/logos/130/4016/thumbs/hy5fvvdkee83gg3r5ym22zr5o.gif",
                LeagueId = 1,
                StadiumName = "Camp Nou"
            };
            teams.Add(team);
            team = new Team
            {
                Id = 2,
                Name = "Real Madrid",
                BadgeUrl = "https://content.sportslogos.net/logos/130/4017/thumbs/yfhezt5oyr0jbq29u4hp50w63.gif",
                LeagueId = 1,
                StadiumName = "Santiago Bernabeu"
            };
            teams.Add(team);
            team = new Team
            {
                Id = 3,
                Name = "Bayern Munich",
                BadgeUrl = "https://content.sportslogos.net/logos/132/4069/thumbs/rr72mhpas38h85jdw85neas5f.gif",
                LeagueId = 2,
                StadiumName = "Allianz Arena"
            };
            teams.Add(team);
            team = new Team
            {
                Id = 4,
                Name = "Borussia Dortmund",
                BadgeUrl = "https://content.sportslogos.net/logos/132/4072/thumbs/yfkihagcptzem3rhhf4h22343.gif",
                LeagueId = 2,
                StadiumName = "Signal Induna Park"
            };
            teams.Add(team);

            return teams.ToArray();
        }
    }
}