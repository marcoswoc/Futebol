using FutebolApi.Models;
using FutebolApi.Models.User;

namespace FutebolApi.Services.Interfaces;

public interface IUserService
{
    Task<ResponseModel> CreateUserAsync(CreateUserModel model, string origin);
    Task<ResponseModel<TokenModel>> LoginAsync(LoginModel model);
    Task<ResponseModel> VerifyAsync(string token);
    Task<ResponseModel> ForgotPassword(string email, string origin);
    Task<ResponseModel> ResetPassword(ResetPasswordModel model);
}
