using FutebolApi.Data.Repositories.Interfaces;
using FutebolApi.Entity;
using FutebolApi.Models;
using FutebolApi.Models.User;
using FutebolApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FutebolApi.Services;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailUserService _emailService;

    public UserService(
        IConfiguration configuration,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IPlayerRepository playerRepository,
        IUserRepository userRepository,
        IEmailUserService emailService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
        _playerRepository = playerRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<ResponseModel> CreateUserAsync(CreateUserModel model, string origin)
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);

        if (userExists is not null)
            return new() { Success = false, Message = "Usuário já existe!" };

        User user = new()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = model.Email,
            UserName = model.UserName,
            VerificationToken = RandomTokenString(),
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded is false)
            return new() { Success = false, Message = string.Join(",", result.Errors.Select(x => x.Description)) };

        await AddToRoleAsync(user, "user");
        await AddPlayerAsync(user);
        await _emailService.SendVerificationEmail(user, origin);

        return new() { Message = "Usuário criado com sucesso. Verifique sua caixa de email para confirmar seu cadastro!" };

    }

    public async Task<ResponseModel<TokenModel>> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
            return new() { Success = false, Message = "Email não cadastrado" };

        if (!user.EmailConfirmed)
            return new() { Success = false, Message = "Email não verificado" };

        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
                authClaims.Add(new(ClaimTypes.Role, userRole));

            return new() { Data = GetToken(authClaims) };
        }

        return new() { Success = false, Message = "Falha ao realizar login" };
    }

    public async Task<ResponseModel> VerifyAsync(string token)
    {
        var user = (await _userRepository.FindExpressionAsync(x => x.VerificationToken == token)).FirstOrDefault();
        if (user is null) return new() { Success = false, Message = "Falha na verificação"};

        user.VerificationToken = null;
        user.EmailConfirmed = true;

        await _userRepository.UpdateAsync(user);

        return new() { Message = "Verificação confirmada" }; 
    }

    public async Task<ResponseModel> ForgotPassword(string email, string origin)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return new() { Success = false, Message = "Email Inválido" };

        user.ResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);        
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

        await _userRepository.UpdateAsync(user);
        await _emailService.SendPasswordResetEmail(user, origin);

        return new() { Message = "Reset realizado, verifique seu email!" };

    }

    public async Task<ResponseModel> ResetPassword(ResetPasswordModel model)
    {
        var user = (await _userRepository.FindExpressionAsync(x =>
            x.ResetToken == model.Token &&
            x.ResetTokenExpires > DateTime.UtcNow)).FirstOrDefault();

        if (user is null) return new() { Success = false, Message = "Token inválido" };

        // update password and remove reset token
        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

        return new() { Success = result.Succeeded, Message = result.Succeeded ? "Senha alterada com sucesso" : "Erro ao alterar senha" };
       
    }


    private async Task AddToRoleAsync(User user, string role)
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

    private string RandomTokenString() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    private async Task AddPlayerAsync(User user)
    {
        var player = new Player { Name = user.UserName, User = user };
        await _playerRepository.CreateAsync(player);
    }   
}
