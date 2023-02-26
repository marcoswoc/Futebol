using Futebol.Shared.Models.User;
using Futebol.Shared.Models;
using System.Net.Http.Json;

namespace Futebol.Web.Services;

public class UserServiceClient
{
    private readonly HttpClient _httpClient;

    public UserServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseModel> CreateUserAsync(CreateUserModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("user/register", model);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        return result;
    }

    public async Task<ResponseModel<TokenModel>> LoginAsync(LoginModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("User/login", model);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<TokenModel>>();
        return result;
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
}
