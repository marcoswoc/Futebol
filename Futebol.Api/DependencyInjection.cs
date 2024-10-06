using Futebol.Api.Database;
using Futebol.Api.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

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
        service.AddScoped<SeedService>();
        return service;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString));

        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }
}
