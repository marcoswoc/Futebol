using Futebol.Shared.Models.Player;

namespace Futebol.Shared.Models.Vote;
public class VoteAvarageModel
{
    public PlayerModel Player { get; set; }
    public IEnumerable<VoteAttributesModel> VoteAttributes { get; set; } = new List<VoteAttributesModel>();
}

public class VoteAttributesModel
{
    public string Name { get; set; }
    public double Value { get; set; }
}
