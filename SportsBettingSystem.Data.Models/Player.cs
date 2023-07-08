namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Player;
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public Position Position { get; set; }

        [Required]
        public int KitNumber { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int Goals { get; set; }

        [Required]
        public int Appearance { get; set; }

        public int TeamId { get; set; }

        public virtual Team? Team { get; set; }
    }
}