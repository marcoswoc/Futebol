namespace Futebol.Application.Models.Round;
public class RoundModel : CreateRoundModel
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
}
