using Futebol.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Futebol.Domain.Entity;
public class User : IdentityUser<Guid>, IEntity
{
    public string VerificationToken { get; set; }
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
