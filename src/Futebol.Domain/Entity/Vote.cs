using Futebol.Infrastructure.Models;

namespace Futebol.Domain.Entity;
public class Vote : EntityBase
{
    public virtual Round Round { get; set; }
    public virtual Player Player { get; set; }
    public virtual User User { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Velocity { get; set; }
    public int Kick { get; set; }
    public int Pass { get; set; }
}
