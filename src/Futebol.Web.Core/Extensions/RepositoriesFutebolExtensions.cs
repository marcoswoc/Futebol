using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Persistence.Contexts;
using Futebol.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Futebol.Web.Core.Extensions;
public static class RepositoriesFutebolExtensions
{
    public static void AddFutebolRepositories(this IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        services.AddScoped(typeof(FutebolDbContext), typeof(FutebolDbContextNpgsql));

        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            opt.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<FutebolDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IPlayerRepository, PlayerRepository<FutebolDbContext>>();
        services.AddScoped<IRoundRepository, RoundRepository<FutebolDbContext>>();
        services.AddScoped<IVoteRepository, VoteRepository<FutebolDbContext>>();
        services.AddScoped<IUserRepository, UserRepository<FutebolDbContext>>();
    }
}