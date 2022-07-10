namespace Futebol.Infrastructure.Email;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string html, string? from = null);
}
