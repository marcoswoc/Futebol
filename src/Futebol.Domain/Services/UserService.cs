using Futebol.Domain.Dto;
using Futebol.Domain.Dto.User;
using Futebol.Domain.Entity;
using Futebol.Domain.Repositories;
using Futebol.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Futebol.Domain.Services;
public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailUserService _emailService;
    public UserService(
        IConfiguration configuration,
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
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

    public async Task<ResponseDto> CreateUserAsync(CreateUserDto dto, string origin)
    {
        var userExists = await _userManager.FindByEmailAsync(dto.Email);

        if (userExists is not null)
            return new() { Success = false, Message = "Usuário já existe!" };

        User user = new()
        {
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = dto.Email,
            UserName = dto.UserName,
            VerificationToken = RandomTokenString(),
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded is false)
            return new() { Success = false, Message = string.Join(",", result.Errors.Select(x => x.Description)) };

        await AddToRoleAsync(user, "user");
        await AddPlayerAsync(user);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            await _emailService.SendVerificationEmail(user, origin);
        else
            await VerifyAsync(user.VerificationToken);

        return new() { Success = true, Message = "Usuário criado com sucesso. Verifique sua caixa de email para confirmar seu cadastro!" };

    }

    public async Task<ResponseDto<TokenDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            return new() { Success = false, Message = "Email não cadastrado" };

        if (!user.EmailConfirmed)
            return new() { Success = false, Message = "Email não verificado" };

        if (await _userManager.CheckPasswordAsync(user, dto.Password))
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

            return new() { Success = true, Data = GetToken(authClaims) };
        }

        return new() { Success = false, Message = "Falha ao realizar login" };
    }

    public async Task<ResponseDto> VerifyAsync(string token)
    {
        var user = (await _userRepository.FindExpressionAsync(x => x.VerificationToken == token)).FirstOrDefault();
        if (user is null) return new() { Success = false, Message = "Falha na verificação" };

        user.VerificationToken = null;
        user.EmailConfirmed = true;

        await _userRepository.UpdateAsync(user);

        return new() { Success = true, Message = "Verificação confirmada" };
    }

    public async Task<ResponseDto> ForgotPassword(string email, string origin)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return new() { Success = false, Message = "Email Inválido" };

        user.ResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

        await _userRepository.UpdateAsync(user);
        await _emailService.SendPasswordResetEmail(user, origin);

        return new() { Success = true, Message = "Reset realizado, verifique seu email!" };

    }

    public async Task<ResponseDto> ResetPassword(ResetPasswordDto dto)
    {
        var user = (await _userRepository.FindExpressionAsync(x =>
            x.ResetToken == dto.Token &&
            x.ResetTokenExpires > DateTime.UtcNow)).FirstOrDefault();

        if (user is null) return new() { Success = false, Message = "Token inválido" };

        // update password and remove reset token
        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);

        return new() { Success = result.Succeeded, Message = result.Succeeded ? "Senha alterada com sucesso" : "Erro ao alterar senha" };

    }


    private async Task AddToRoleAsync(User user, string role)
    {
        if (await _roleManager.RoleExistsAsync(role) is false)
            await _roleManager.CreateAsync(new(role));

        await _userManager.AddToRoleAsync(user, role);
    }

    private TokenDto GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

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
