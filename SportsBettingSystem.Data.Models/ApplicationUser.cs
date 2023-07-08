namespace SportsBettingSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Bets = new HashSet<Bet>();
        }
        public int WalletBallance { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
