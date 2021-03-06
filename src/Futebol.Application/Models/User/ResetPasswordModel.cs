using System.ComponentModel.DataAnnotations;

namespace Futebol.Application.Models.User;
public class ResetPasswordModel
{
    [Required]
    public string Token { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
