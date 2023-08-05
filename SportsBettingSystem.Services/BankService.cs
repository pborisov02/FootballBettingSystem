using Microsoft.EntityFrameworkCore;
using SportsBettingSystem.Data;
using SportsBettingSystem.Data.Models;
using SportsBettingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Services
{
	public class BankService : IBankService
	{
		private readonly SportsBettingDbContext _db;
		public BankService(SportsBettingDbContext db)
		{
			_db = db;
		}
		/// <summary>
		/// Adds the deposit to the user's wallet ballance
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="deposit"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public async Task AddDepositAsync(string userId, decimal deposit)
		{
			ApplicationUser? user = await _db.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

			if(user == null)
			{
				throw new InvalidOperationException("There is no user with this Id");
			}

			user.WalletBallance += deposit;
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Withdraws the ammount of the user's wallet
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="ammount"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public async Task WithdrawAsync(Guid userId, decimal ammount)
		{
			ApplicationUser? user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
			{
				throw new InvalidOperationException("There is no user with this Id");
			}

			user.WalletBallance -= ammount;
			await _db.SaveChangesAsync();
		}
	}
}
