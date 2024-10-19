using Futebol.Api.Entities.Account;
using Futebol.Shared.Models;

namespace Futebol.Api.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public PlayerModel Model()
    {
        return new PlayerModel()
        {
            Id = Id,
            Name = Name
        };
    }
}
