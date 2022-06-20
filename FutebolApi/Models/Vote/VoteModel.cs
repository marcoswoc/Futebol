using FutebolApi.Models.Player;
using FutebolApi.Models.Round;

namespace FutebolApi.Models.Vote;

public class VoteModel : UpdateVoteModel
{
    public Guid Id { get; set; }
    public RoundModel Round { get; set; }
    public PlayerModel Player { get; set; }
}
