using Futebol.Shared.Models.User;
using Futebol.Shared.Models;
using System.Net.Http.Json;
using Futebol.Web.JwtService;
using Futebol.Web.AuthenticationProvider;

namespace Futebol.Web.Services;

public class UserServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;
    private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;

    public UserServiceClient(HttpClient httpClient, ITokenService tokenService, CustomAuthenticationStateProvider customAuthenticationStateProvider)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
        _customAuthenticationStateProvider = customAuthenticationStateProvider;
    }

    public async Task<ResponseModel> CreateUserAsync(CreateUserModel model)
    {
        await SetTokenRquest();
        var response = await _httpClient.PostAsJsonAsync("user/register", model);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();        
        return result;
    }

    public async Task<ResponseModel<TokenModel>> LoginAsync(LoginModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("User/login", model);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<TokenModel>>();
        await _tokenService.SetTokenAsync(result.Data);
        _customAuthenticationStateProvider.StateChanged();
        return result;
    }

    public async Task LogoutUser()
    {
        await _tokenService.RemoveTokenAsync();
        _customAuthenticationStateProvider.StateChanged();
    }

    public async Task<ResponseModel> VerifyAsync(string token)
    {
        var response = await _httpClient.PostAsJsonAsync("user/verify-email", token);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        return result;
    }

    public async Task<ResponseModel> ForgotPassword(string email)
    {
        var response = await _httpClient.PostAsJsonAsync("user/forgot-password", email);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        return result;
    }

    public async Task<ResponseModel> ResetPassword(ResetPasswordModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("user/reset-password", model);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        return result;
    }

    private async Task SetTokenRquest()
    {
        var token = await _tokenService.GetTokenAsync();

        if (token is not null && token.ValidTo > DateTime.Now)
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", $"{token.Token}");
    }
}
