using System.ComponentModel.DataAnnotations;

namespace FutebolApi.Models.UserModel;

public class CreateUserModel : LoginModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} é obrigatório!")]
    public string UserName { get; set; }
}
