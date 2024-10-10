using System.ComponentModel.DataAnnotations;

namespace Futebol.Shared.Requests.Team;
public class UpdateTeamRequest
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome Obrigatório")]
    [MaxLength(200, ErrorMessage = "Nome deve conter até {0} caracteres")]
    public string Name { get; set; } = string.Empty;
}
