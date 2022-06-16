using System.ComponentModel.DataAnnotations;

namespace FutebolApi.Models.User;

public class LoginModel
{
    [Required(ErrorMessage = $"{nameof(Email)} é obrigatório!")]
    public string Email { get; set; }

    [Required(ErrorMessage = $"{nameof(Password)} é obrigatório!")]
    public string Password { get; set; }
}
