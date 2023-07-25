
namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Bet
    {
        public Bet()
        {
            this.Id = Guid.NewGuid();
            this.GameBets = new List<GameBet>();
        }
        
        [Key]
        public Guid Id { get; set; }
        public virtual ICollection<GameBet> GameBets { get; set; }
        public bool IsWinning { get; set; }
        public decimal Multiplier { get; set; }
        public decimal BetAmmount { get; set; }
        public bool IsDone { get; set; }

        [Required]
        public Guid UserId { get; set;}

        [Required]
        public ApplicationUser User { get; set; } = null!;
		
	}

}
