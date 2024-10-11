using Microsoft.AspNetCore.Identity;

namespace Futebol.Api.Entities.Account;

public class User : IdentityUser<Guid>
{
    public Player Player { get; set; } = null!;
}
