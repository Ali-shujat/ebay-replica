using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

public class BuyerLoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

}
