using Microsoft.AspNetCore.Identity;

namespace FutebolApi.Entity;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IdentityUser User { get; set; }
}
