using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportsBettingSystem.Data;
using SportsBettingSystem.Data.Models;
using SportsBettingSystem.Services.Interfaces;
using SportsBettingSystem.Web.ViewModels.Game;
using SportsBettingSystem.Web.ViewModels.League;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Services
{
	public class LeagueService : ILeagueService
	{
		private readonly SportsBettingDbContext _db;
		public LeagueService(SportsBettingDbContext db)
		{
			_db = db;
		}

		public async Task CreateAsync(LeagueFormModel leagueFormModel)
		{
			await _db.Leagues.AddAsync(new League
			{
				Name = leagueFormModel.Name,
				Country = leagueFormModel.Country,
				LogoUrl = leagueFormModel.LogoUrl
			});

			await _db.SaveChangesAsync();
		}
		public async Task<IEnumerable<LeagueServiceModel>> AllLeaguesAsync()
		{
			return await _db.Leagues
				.Select(l => new LeagueServiceModel
				{
					Id = l.Id,
					Name = l.Name
				}).ToListAsync();
		}
	}
}
