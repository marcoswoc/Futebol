using Futebol.Shared.Models.Player;
using Futebol.Shared.Models.Round;

namespace Futebol.Shared.Models.Vote;
public class VoteModel : UpdateVoteModel
{
    public Guid Id { get; set; }
    public RoundModel Round { get; set; }
    public PlayerModel Player { get; set; }
}
