using Futebol.Api.Entities.Account;

namespace Futebol.Api.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
