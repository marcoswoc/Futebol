using System.ComponentModel.DataAnnotations;

namespace Futebol.Infrastructure.Models;
public abstract class EntityBase : IEntity, IAuditable
{
    [Key]
    [Required]
    public virtual Guid Id { get; set; } = Guid.NewGuid();
    public virtual DateTime? CreatedAt { get; set; }
    public virtual DateTime? UpdatedAt { get; set; }
    public virtual DateTime? DeletedAt { get; set; }
}
