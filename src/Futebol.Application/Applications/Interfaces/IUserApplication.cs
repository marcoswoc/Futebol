using Futebol.Shared.Models;
using Futebol.Shared.Models.User;

namespace Futebol.Application.Applications.Interfaces;
public interface IUserApplication
{
    Task<ResponseModel> CreateUserAsync(CreateUserModel model, string origin);
    Task<ResponseModel<TokenModel>> LoginAsync(LoginModel model);
    Task<ResponseModel> VerifyAsync(string token);
    Task<ResponseModel> ForgotPassword(string email, string origin);
    Task<ResponseModel> ResetPassword(ResetPasswordModel model);
}
