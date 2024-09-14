using System.ComponentModel.DataAnnotations;

namespace TMS.API.DTO;

public class RegisterUserDto
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
}

public class LoginUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}