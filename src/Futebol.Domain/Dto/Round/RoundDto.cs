namespace Futebol.Domain.Dto.Round;
public class RoundDto : CreateRoundDto
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public int Number { get; set; }
}
