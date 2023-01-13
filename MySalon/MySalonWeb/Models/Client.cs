using System.ComponentModel.DataAnnotations;

namespace MySalonWeb.Models
{
    public class Client
    {
        public int Id { get; set; } = 0!;

        [Required(ErrorMessage = "Enter your name.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Enter your surename.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Phone { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
