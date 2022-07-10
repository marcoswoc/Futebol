using Futebol.Domain.Entity;

namespace Futebol.Domain.Services.Interfaces;
public interface IEmailUserService
{
    Task SendVerificationEmail(User user, string origin);
    Task SendPasswordResetEmail(User user, string origin);
}
