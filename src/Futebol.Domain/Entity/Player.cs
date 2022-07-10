using Futebol.Infrastructure.Models;

namespace Futebol.Domain.Entity;
public class Player : EntityBase
{    
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public virtual User User { get; set; }
}
