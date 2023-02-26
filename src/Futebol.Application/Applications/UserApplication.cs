using AutoMapper;
using Futebol.Application.Applications.Interfaces;
using Futebol.Domain.Dto.User;
using Futebol.Domain.Services.Interfaces;
using Futebol.Shared.Models;
using Futebol.Shared.Models.User;

namespace Futebol.Application.Applications;
public class UserApplication : IUserApplication
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserApplication(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateUserAsync(CreateUserModel model, string origin)
    {
        var dto = await _userService.CreateUserAsync(_mapper.Map<CreateUserDto>(model), origin);
        return _mapper.Map<ResponseModel>(dto);
    }

    public async Task<ResponseModel> ForgotPassword(string email, string origin)
    {
        var dto = await _userService.ForgotPassword(email, origin);
        return _mapper.Map<ResponseModel>(dto);
    }

    public async Task<ResponseModel<TokenModel>> LoginAsync(LoginModel model)
    {
        var dto = await _userService.LoginAsync(_mapper.Map<LoginDto>(model));
        return _mapper.Map<ResponseModel<TokenModel>>(dto);
    }

    public async Task<ResponseModel> ResetPassword(ResetPasswordModel model)
    {
        var dto = await _userService.ResetPassword(_mapper.Map<ResetPasswordDto>(model));
        return _mapper.Map<ResponseModel>(dto);
    }

    public async Task<ResponseModel> VerifyAsync(string token)
    {
        var dto = await _userService.VerifyAsync(token);
        return _mapper.Map<ResponseModel>(dto);
    }
}
