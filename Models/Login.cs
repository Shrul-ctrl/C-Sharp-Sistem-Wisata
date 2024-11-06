using System.ComponentModel.DataAnnotations;

namespace sistem_wisata.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

    }
}
