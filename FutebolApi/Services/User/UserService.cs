using FutebolApi.Models;
using FutebolApi.Models.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FutebolApi.Services.User;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(
        IConfiguration configuration,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ResponseModel> CreateUserAsync(CreateUserModel model)
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);

        if (userExists is not null)
            return new() { Success = false, Message = "Usuário já existe!" };

        IdentityUser user = new()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = model.Email,
            UserName = model.UserName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded is false)
            return new() { Success = false, Message = string.Join(",", result.Errors.Select(x => x.Description)) };

        await AddToRoleAsync(user, "user");

        return new() { Message = "Usuário criado com sucesso!" };

    }

    public async Task<ResponseModel> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
                authClaims.Add(new(ClaimTypes.Role, userRole));

            return new() { Data = GetToken(authClaims) };
        }

        return new() { Success = false };
    }

    private async Task AddToRoleAsync(IdentityUser user, string role)
    {
        if (await _roleManager.RoleExistsAsync(role) is false)
            await _roleManager.CreateAsync(new(role));

        await _userManager.AddToRoleAsync(user, role);
    }

    private TokenModel GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new() { Token = new JwtSecurityTokenHandler().WriteToken(token), ValidTo = token.ValidTo };
    }
}
