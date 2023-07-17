using Futebol.Application.Applications;
using Futebol.Application.Applications.Interfaces;
using Futebol.Domain.Services;
using Futebol.Domain.Services.Interfaces;
using Futebol.Infrastructure.Email;
using Futebol.Infrastructure.Upload;
using Microsoft.Extensions.DependencyInjection;

namespace Futebol.Web.Core.Extensions;
public static class ServiceFutebolExtensions
{
    public static void AddFutebolServices(this IServiceCollection services)
    {
        services.AddScoped<IPlayerApplication, PlayerApplication>();
        services.AddScoped<IRoundApplication, RoundApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<IVoteApplication, VoteApplication>();


        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IRoundService, RoundService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVoteService, VoteService>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailUserService, EmailUserService>();
        services.AddScoped<IUploadImage, UploadImageService>();
    }
}
