namespace Futebol.Application.Models.Round;
public class RoundModel : CreateRoundModel
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public int Number { get; set; }
}
