using Futebol.Domain.Entity;
using Futebol.Domain.Services.Interfaces;
using Futebol.Infrastructure.Email;

namespace Futebol.Domain.Services;
public class EmailUserService : IEmailUserService
{
    private readonly IEmailService _email;

    public EmailUserService(IEmailService email)
    {
        _email = email;
    }

    public async Task SendPasswordResetEmail(User user, string origin)
    {
        string message;
        if (!string.IsNullOrEmpty(origin))
        {
            var resetUrl = $"{origin}/account/reset-password?token={user.ResetToken}";
            message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
        }
        else
        {
            message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{user.ResetToken}</code></p>";
        }

        await _email.SendEmailAsync(
             to: user.Email,
             subject: "Sign-up Verification API - Reset Password",
             html: $@"<h4>Reset Password Email</h4>
                         {message}"
         );
    }

    public async Task SendVerificationEmail(User user, string origin)
    {
        string message;
        if (!string.IsNullOrEmpty(origin))
        {
            var verifyUrl = $"{origin}/account/verify-email?token={user.VerificationToken}";
            message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
        }
        else
        {
            message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{user.VerificationToken}</code></p>";
        }

        await _email.SendEmailAsync(
            to: user.Email,
            subject: "Sign-up Verification API - Verify Email",
            html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
        );
    }
}
