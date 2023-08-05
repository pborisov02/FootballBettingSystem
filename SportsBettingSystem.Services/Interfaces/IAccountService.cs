namespace SportsBettingSystem.Services.Interfaces
{
    using SportsBettingSystem.Data.Models;
    using SportsBettingSystem.Web.ViewModels.Account;
    public interface IAccountService
    {
        Task<AccountViewModel> AccountInfo(Guid userId);
        Task<ApplicationUser> GetUser(string userId);
        Task UpdateUserWallet(Guid userId, decimal winning);
    }
}
