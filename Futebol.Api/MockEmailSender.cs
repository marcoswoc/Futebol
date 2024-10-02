using Microsoft.AspNetCore.Identity.UI.Services;

namespace Futebol.Api;

public class MockEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"Email para: {email}, Assunto: {subject}");
        return Task.CompletedTask;
    }
}
