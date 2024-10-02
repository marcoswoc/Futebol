using Futebol.Api.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Futebol.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddFutebolApi(this IServiceCollection services, IConfiguration configuration)
        => services
        .AddAuthenticationInternal()
        .AddAuthorizationInternal()
        .AddDatabase(configuration)
        .AddCrossOrigin()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddIdentityEndpoints()
        .AddServices();            

    public static IServiceCollection AddCrossOrigin(this IServiceCollection services)
    {
        services.AddCors(
            options => options.AddPolicy(
                "futebolApi",
                policy => policy
                    .WithOrigins(
                        "http://localhost:5029",
                        "https://localhost:7283"
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                ));

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddSingleton<IEmailSender, MockEmailSender>();

        return service;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthentication().AddIdentityCookies();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddIdentityEndpoints(this IServiceCollection services)
    {
        services
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}
