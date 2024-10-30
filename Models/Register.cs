using System.ComponentModel.DataAnnotations;

namespace sistem_wisata.Models;

public class Register
{
    [Required]
    public string Nama { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password dan konfirmasi tidak cocok.")]
    public string ConfirmPassword { get; set; }
}

