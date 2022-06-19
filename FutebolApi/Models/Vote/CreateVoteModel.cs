namespace FutebolApi.Models.Vote;

public class CreateVoteModel
{
    public Guid RoundId { get; set; }
    public Guid PlayerId { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Velocity { get; set; }
    public int Kick { get; set; }
    public int Pass { get; set; }
}
