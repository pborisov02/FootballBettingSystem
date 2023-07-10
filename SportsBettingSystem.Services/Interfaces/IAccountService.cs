namespace SportsBettingSystem.Services.Interfaces
{
    using SportsBettingSystem.Web.ViewModels.Account;
    public interface IAccountService
    {
        Task<AccountViewModel> DisplayAccountInfo(Guid userId);
    }
}
