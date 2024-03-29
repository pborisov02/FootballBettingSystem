﻿namespace SportsBettingSystem.Services
{
	using Microsoft.EntityFrameworkCore;

	using SportsBettingSystem.Data;
	using SportsBettingSystem.Data.Models;
	using SportsBettingSystem.Services.Interfaces;
	using SportsBettingSystem.Web.ViewModels.Team;
	using System.Threading.Tasks;

	public class TeamService : ITeamService
	{
		private readonly SportsBettingDbContext _db;
		public TeamService(SportsBettingDbContext dbContext)
		{
			this._db = dbContext;
		}

		public async Task CreateTeamAsync(TeamFormModel model)
		{
			var team = new Team()
			{
				Name = model.Name,
				LeagueId = model.LeagueId
			};

			await _db.Teams.AddAsync(team);
			await _db.SaveChangesAsync();
		}

		public async Task<bool> LeagueExistsAsync(int leagueId)
		{
			return await _db.Leagues.AnyAsync(l => l.Id == leagueId);
		}
	}
}
