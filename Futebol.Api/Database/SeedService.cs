using Futebol.Api.Entities.Account;
using Microsoft.AspNetCore.Identity;

namespace Futebol.Api.Database;

public class SeedService(
    UserManager<User> _userManager, 
    RoleManager<Role> _roleManager,
    IConfiguration _configuration)
{
    public async Task SeedRolesAndUsersAsync()
    {
        var roles = _configuration.GetSection("UserSeedData:Roles").Get<List<RoleSeedData>>() ?? [];
        var users = _configuration.GetSection("UserSeedData:Users").Get<List<UserSeedData>>() ?? [];

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role.Name))            
                await _roleManager.CreateAsync(new() { Name = role.Name });
            
        }

        foreach (var user in users)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                var newUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)                
                    await _userManager.AddToRoleAsync(newUser, user.Role);                
            }
        }
    }
}

public class RoleSeedData
{
    public required string Name { get; set; }
}

public class UserSeedData
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}
