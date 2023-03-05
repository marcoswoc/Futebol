using Futebol.Shared.Models.Round;
using Futebol.Shared.Models;
using Futebol.Web.JwtService;
using System.Net.Http.Json;

namespace Futebol.Web.Services;

public class RoundServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    public RoundServiceClient(HttpClient httpClient, ITokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    public async Task<ResponseModel<IEnumerable<RoundModel>>> GetAllAsync()
    {
        await SetTokenRquest();
        var response = await _httpClient.GetAsync("round");
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<IEnumerable<RoundModel>>>();
        return result;
    }

    public async Task<ResponseModel<RoundModel>> GetByIdAsync(Guid id)
    {
        await SetTokenRquest();
        var response = await _httpClient.GetAsync($"round/{id}");
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<RoundModel>>();
        return result;
    }

    public async Task<ResponseModel<RoundModel>> CreateAsync(CreateRoundModel model)
    {
        await SetTokenRquest();
        var response = await _httpClient.PostAsJsonAsync("round", model);
        var result = await response.Content.ReadFromJsonAsync<ResponseModel<RoundModel>>();
        return result;
    }

    private async Task SetTokenRquest()
    {
        var token = await _tokenService.GetTokenAsync();

        if (token is not null && token.ValidTo > DateTime.Now)
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", $"{token.Token}");
    }

}
