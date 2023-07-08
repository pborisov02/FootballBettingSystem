namespace SportsBettingSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SportsBettingSystem.Data.Models;

    public class SportsBettingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SportsBettingDbContext(DbContextOptions<SportsBettingDbContext> options)
            : base(options)
        {
        }
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<League> Leagues { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Bet> Bets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(c => c.Players)
                .HasForeignKey(p => p.TeamId)
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

            builder.Entity<Bet>()
                .HasOne(b => b.Game)
                .WithMany(g => g.Bets)
                .HasForeignKey(b => b.GameId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.League)
                .HasForeignKey(t => t.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<League>().HasData(GenerateLeagues());
            builder.Entity<Team>().HasData(GenerateTeams());
            builder.Entity<Player>().HasData(GeneratePlayers());

            base.OnModelCreating(builder);

        }

        private League[] GenerateLeagues()
        {
            List<League> leagues = new List<League>();
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

        private Team[] GenerateTeams()
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

        private Player[] GeneratePlayers()
        {
            var players = new List<Player>();

            var player = new Player
            {
                Id = 1,
                FirstName = "Pablo",
                LastName = "Gavi",
                Position = Position.CAM,
                KitNumber = 30,
                Age = 18,
                TeamId = 1
            };
            players.Add(player);

            player = new Player
            {
                Id = 2,
                FirstName = "Vinicius",
                LastName = "Jr",
                Position = Position.LW,
                KitNumber = 22,
                Age = 22,
                TeamId = 2
            };
            players.Add(player);

            return players.ToArray();
        }
    }
}