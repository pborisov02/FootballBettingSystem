namespace SportsBettingSystem.Services.Interfaces
{
    using SportsBettingSystem.Data.Models;
    using SportsBettingSystem.Web.ViewModels.Account;
    public interface IAccountService
    {
        Task<AccountViewModel> AccountInfoAsync(Guid userId);
        Task UpdateUserWalletBalance(Guid userId, decimal winning);
    }
}
