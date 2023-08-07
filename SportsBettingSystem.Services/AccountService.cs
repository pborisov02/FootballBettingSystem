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
        /// <summary>
        /// Returns the viewmodel with the account info for the account that matches the id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AccountViewModel> AccountInfoAsync(Guid userId)
        {

            ApplicationUser? user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null)
            {
                throw new InvalidDataException();
            }

            AccountViewModel accountViewModel = new AccountViewModel
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                CreatedOn = user.CreatedOn,
                WalletBalance = user.WalletBalance
            };

            return accountViewModel;
        }

        /// <summary>
        /// Updates the wallet ballance of the user that matches the given id by adding the winning ammount to it's wallet
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="winning"></param>
        /// <returns></returns>
		public async Task UpdateUserWalletBalance(Guid userId, decimal winning)
		{
            ApplicationUser user = await dbContext.Users.FirstAsync(u => u.Id == userId);
            user.WalletBalance += winning;
            await dbContext.SaveChangesAsync();
		}
	}
}
