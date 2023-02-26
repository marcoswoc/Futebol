using System.ComponentModel.DataAnnotations;

namespace Futebol.Application.Models.User;
public class LoginModel
{
    [Required(ErrorMessage = $"{nameof(Email)} é obrigatório!")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = $"{nameof(Password)} é obrigatório!")]
    public string Password { get; set; }
}
