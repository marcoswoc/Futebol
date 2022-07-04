using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FutebolApi.Infra;

public class EmailSender : IEmailSender
{

    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration,
                      ILogger<EmailSender> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task SendEmailAsync(string to, string subject, string html, string from = null)
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

        var response = await client.SendEmailAsync(msg);
        _logger.LogInformation(response.IsSuccessStatusCode ? $"Email queued successfully!" : $"Failure Email");
    }
}
