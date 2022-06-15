using Microsoft.AspNetCore.Identity;

namespace FutebolApi.Data;

public static class SeedData
{
    public async static Task Initialize(IServiceProvider servicesProvider, IConfiguration configuration)
    {
        var adminId = configuration["UserConfigurations:AdminId"];
        var roleId = configuration["UserConfigurations:RoleId"];
        var email = configuration["UserConfigurations:Email"];
        var password = configuration["UserConfigurations:Password"];

        var userManager = servicesProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>();        

        if (await userManager.FindByEmailAsync(email) is null)
        {

            var role = new IdentityRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin".ToUpper()
            };

            await roleManager.CreateAsync(role);

            var hasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, password),
                SecurityStamp = string.Empty
            };

            await userManager.CreateAsync(user);

            await userManager.AddToRoleAsync(user,role.Name);            
        }
    }
}
