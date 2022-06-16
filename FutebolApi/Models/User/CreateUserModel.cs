using System.ComponentModel.DataAnnotations;

namespace FutebolApi.Models.User;

public class CreateUserModel : LoginModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} é obrigatório!")]
    public string UserName { get; set; }
}
