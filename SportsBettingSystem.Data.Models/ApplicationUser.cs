namespace SportsBettingSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using SportsBettingSystem.Web.ViewModels;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Bets = new HashSet<Bet>();
        }
        public decimal WalletBallance { get; set; }
        [MaxLength(15)]
        public string FirstName { get; set; } = null!;
		[MaxLength(15)]
		public string LastName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
