namespace SportsBettingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}