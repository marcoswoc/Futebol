using FutebolApi.Models;
using FutebolApi.Models.User;

namespace FutebolApi.Services.Interfaces;

public interface IUserService
{
    Task<ResponseModel> CreateUserAsync(CreateUserModel model);
    Task<ResponseModel> LoginAsync(LoginModel model);
}
