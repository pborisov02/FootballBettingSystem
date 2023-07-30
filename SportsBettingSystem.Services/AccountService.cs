using Microsoft.EntityFrameworkCore;
using SportsBettingSystem.Data;
using SportsBettingSystem.Data.Models;
using SportsBettingSystem.Services.Interfaces;
using SportsBettingSystem.Web.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Services
{
    public class AccountService : IAccountService
    {
        private SportsBettingDbContext dbContext;

        public AccountService(SportsBettingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<AccountViewModel> DisplayAccountInfo(Guid userId)
        {

            ApplicationUser user = await dbContext.Users.AsNoTracking().FirstAsync(u => u.Id == userId);

            AccountViewModel accountViewModel = new AccountViewModel
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                WalletBalance = user.WalletBallance
            };

            return accountViewModel;
        }

		public async Task<ApplicationUser> GetUser(string userId)
		{
			return await dbContext.Users.AsNoTracking().FirstAsync(u => u.Id == Guid.Parse(userId));
		}

		public async Task UpdateUserWallet(Guid userId, decimal winning)
		{
            ApplicationUser user = await dbContext.Users.FirstAsync(u => u.Id == userId);
            user.WalletBallance += winning;
            await dbContext.SaveChangesAsync();
		}
	}
}
