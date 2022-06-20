using Microsoft.AspNetCore.Identity;

namespace FutebolApi.Entity;

public class Vote
{
    public Guid Id { get; set; }
    public virtual Round Round { get; set; }
    public virtual Player Player { get; set; }
    public virtual IdentityUser User { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Velocity { get; set; }
    public int Kick { get; set; }
    public int Pass { get; set; }
}
