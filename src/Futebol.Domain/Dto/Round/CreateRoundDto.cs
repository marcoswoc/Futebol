namespace Futebol.Domain.Dto.Round;
public class CreateRoundDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Number { get; set; }
}
