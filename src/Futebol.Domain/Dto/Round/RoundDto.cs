namespace Futebol.Domain.Dto.Round;
public class RoundDto : CreateRoundDto
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
}
