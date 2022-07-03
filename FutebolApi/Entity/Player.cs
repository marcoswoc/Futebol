using Microsoft.AspNetCore.Identity;

namespace FutebolApi.Entity;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public virtual IdentityUser User { get; set; }
}
