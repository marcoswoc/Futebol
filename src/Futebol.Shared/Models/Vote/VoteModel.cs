using Futebol.Application.Models.Player;
using Futebol.Application.Models.Round;

namespace Futebol.Application.Models.Vote;
public class VoteModel : UpdateVoteModel
{
    public Guid Id { get; set; }
    public RoundModel Round { get; set; }
    public PlayerModel Player { get; set; }
}
