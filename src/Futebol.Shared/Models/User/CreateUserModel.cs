using System.ComponentModel.DataAnnotations;

namespace Futebol.Shared.Models.User;
public class CreateUserModel : LoginModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} é obrigatório!")]
    public string UserName { get; set; }

    [Required(ErrorMessage = $"{nameof(ConfirmPassword)} é obrigatório!")]
    [Compare($"{nameof(Password)}", ErrorMessage = $"{nameof(ConfirmPassword)} não é o mesmo que {nameof(Password)}")]
    public string ConfirmPassword { get; set; }
}
