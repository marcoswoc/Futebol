namespace Futebol.Domain.Dto.Vote;
public class CreateVoteDto : UpdateVoteDto
{
    public Guid RoundId { get; set; }
    public Guid PlayerId { get; set; }
}
