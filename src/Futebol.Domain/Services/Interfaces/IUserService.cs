using Futebol.Domain.Dto;
using Futebol.Domain.Dto.User;

namespace Futebol.Domain.Services.Interfaces;
public interface IUserService
{
    Task<ResponseDto> CreateUserAsync(CreateUserDto dto, string origin);
    Task<ResponseDto<TokenDto>> LoginAsync(LoginDto dto);
    Task<ResponseDto> VerifyAsync(string token);
    Task<ResponseDto> ForgotPassword(string email, string origin);
    Task<ResponseDto> ResetPassword(ResetPasswordDto dto);
}
