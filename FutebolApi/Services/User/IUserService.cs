using FutebolApi.Models;
using FutebolApi.Models.UserModel;

namespace FutebolApi.Services.User;

public interface IUserService
{
    Task<ResponseModel> CreateUserAsync(CreateUserModel model);
    Task<ResponseModel> LoginAsync(LoginModel model);
}
