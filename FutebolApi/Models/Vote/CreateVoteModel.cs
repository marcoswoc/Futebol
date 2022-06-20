namespace FutebolApi.Models.Vote;

public class CreateVoteModel : UpdateVoteModel
{
    public Guid RoundId { get; set; }
    public Guid PlayerId { get; set; }
}
