using Futebol.Domain.Entity;
using Futebol.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futebol.Web.Core.Extensions;
public static class SeedDataFutebolExtensions
{
    public static void AddSeed(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<FutebolDbContext>();
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
            dbContext.Database.Migrate();      

    }

    public async static Task Initialize(this IServiceCollection services, IConfiguration configuration)
    {
        var adminId = configuration["UserConfigurations:AdminId"];
        var roleId = configuration["UserConfigurations:RoleId"];
        var email = configuration["UserConfigurations:Email"];
        var password = configuration["UserConfigurations:Password"];


        var serviceProvider = services.BuildServiceProvider();

        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        if (await userManager.FindByEmailAsync(email) is null)
        {

            var role = new IdentityRole<Guid>
            {
                Id = Guid.Parse(roleId),
                Name = "admin",
                NormalizedName = "admin".ToUpper()
            };

            await roleManager.CreateAsync(role);

            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Id = Guid.Parse(adminId),
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, password),
                SecurityStamp = string.Empty
            };

            await userManager.CreateAsync(user);

            await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}
