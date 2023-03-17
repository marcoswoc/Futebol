using Futebol.Shared.Models.Player;
using Futebol.Shared.Models;
using Futebol.Web.JwtService;
using System.Net.Http.Json;

namespace Futebol.Web.Services;

public class PlayerServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    public PlayerServiceClient(HttpClient httpClient, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    public async Task<ResponseModel<IEnumerable<PlayerModel>>> GetAllAsync()
    {
        await SetTokenRquest();
        var response = await _httpClient.GetAsync("player");
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<IEnumerable<PlayerModel>>>();
        return result;

    }

    public async Task<ResponseModel<PlayerModel>> GetByIdAsync(Guid id)
    {
        await SetTokenRquest();
        var response = await _httpClient.GetAsync($"player/{id}");
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<PlayerModel>>();
        return result;
    }

    private async Task SetTokenRquest()
    {
        var token = await _tokenService.GetTokenAsync();

        if (token is not null && token.ValidTo > DateTime.Now)
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", $"{token.Token}");
    }
    //Task<ResponseModel<string>> UploadImageAsync(IFormFile model);
}
