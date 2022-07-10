using Futebol.Domain.Dto.Player;

namespace Futebol.Domain.Dto.Vote;
public class VoteAvarageDto
{
    public PlayerDto Player { get; set; }
    public double Attack { get; set; }
    public double Defense { get; set; }
    public double Velocity { get; set; }
    public double Kick { get; set; }
    public double Pass { get; set; }
    public double GeneralAverage { get; set; }
}
