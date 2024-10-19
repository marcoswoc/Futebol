using System.ComponentModel.DataAnnotations;

namespace Futebol.Shared.Requests.Player;
public class CreatePlayerRequest
{
    [Required(ErrorMessage = "Nome Obrigatório")]
    [MaxLength(200, ErrorMessage = "Nome deve conter até {0} caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail Obrigatório")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha Obrigatória")]
    public string Password { get; set; } = string.Empty;
}