namespace FutebolApi.Infra;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string html, string from = null);
}
