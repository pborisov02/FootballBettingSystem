﻿namespace SportsBettingSystem.Services.Interfaces
{
    using SportsBettingSystem.Data.Models;
    using SportsBettingSystem.Web.ViewModels.Account;
    public interface IAccountService
    {
        Task<AccountViewModel> DisplayAccountInfo(Guid userId);
        Task<ApplicationUser> GetUser(string userId);

    }
}
