namespace SportsBettingSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Bets = new HashSet<Bet>();
        }
        public decimal WalletBallance { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
