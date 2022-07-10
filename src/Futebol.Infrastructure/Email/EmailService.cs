using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Futebol.Infrastructure.Email;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmailAsync(string to, string subject, string html, string? from = null)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_configuration["SendGrid:Email"]),
            Subject = subject,
            PlainTextContent = html,
            HtmlContent = html
        };

        msg.AddTo(new EmailAddress(to));

        await ExecuteAsync(msg);
    }

    private async Task ExecuteAsync(SendGridMessage msg)
    {
        var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);

        await client.SendEmailAsync(msg);        
    }
}
