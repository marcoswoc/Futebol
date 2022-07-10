using Futebol.Domain.Dto.Player;
using Futebol.Domain.Dto.Round;

namespace Futebol.Domain.Dto.Vote;
public class VoteDto : UpdateVoteDto
{
    public Guid Id { get; set; }
    public RoundDto Round { get; set; }
    public PlayerDto Player { get; set; }
}
