using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.UserDto;

public class UserRegisterRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Role { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters, dude!")]
    public string Password { get; set; } = string.Empty;
    [Required, Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}