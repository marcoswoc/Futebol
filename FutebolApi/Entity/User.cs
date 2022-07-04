using Microsoft.AspNetCore.Identity;

namespace FutebolApi.Entity;

public class User : IdentityUser
{
    public string VerificationToken { get; set; }
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
}
