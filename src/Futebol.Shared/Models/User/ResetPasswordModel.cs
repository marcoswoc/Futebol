using System.ComponentModel.DataAnnotations;

namespace Futebol.Shared.Models.User;
public class ResetPasswordModel : ModelBase
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
