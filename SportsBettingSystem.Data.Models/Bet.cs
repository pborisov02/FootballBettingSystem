
namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Bet
    {
        public Bet()
        {
            this.Id = Guid.NewGuid();
        }
        
        [Key]
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public int Prediction { get; set; }
        public bool IsWinning { get; set; }

        [Required]
        public Guid UserId { get; set;}

        [Required]
        public ApplicationUser User { get; set; }
    }
}
