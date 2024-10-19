using Futebol.Shared.Handlers;
using Futebol.Shared.Models;
using Futebol.Shared.Requests.Player;
using Futebol.Shared.Responses;
using System.Net.Http.Json;

namespace Futebol.Web.Handlers;

public class PlayerHandler(IHttpClientFactory httpClientFactory) : IPlayerHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient("futebol");
    private const string _api = "v1/players";

    public async Task<Response<PlayerModel?>> CreateAsync(CreatePlayerRequest request)
    {
        var result = await _client.PostAsJsonAsync(_api, request);
        return await result.Content.ReadFromJsonAsync<Response<PlayerModel?>>()
            ?? new(null, 400, "Falha ao criar o jogador");
    }
}
