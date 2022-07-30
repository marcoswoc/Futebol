using Futebol.Domain.Dto.Player;

namespace Futebol.Domain.Dto.Vote;
public class VoteAvarageDto
{
    public PlayerDto Player { get; set; }
    public IEnumerable<VoteAttributesDto> VoteAttributes { get; set; } = new List<VoteAttributesDto>();
}

public class VoteAttributesDto
{
    public string Name { get; set; }
    public double Value { get; set; }
}
