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

    public async Task<PagedResponse<List<PlayerModel>?>> GetAllAsync(GetAllPlayersRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<PlayerModel>?>>(_api)
        ?? new(null, 400, "Não foi possível obter os jogadores");

    public async Task<Response<PlayerModel?>> GetByIdAsync(GetByIdPlayerRequest request)
        => await _client.GetFromJsonAsync<Response<PlayerModel?>>($"{_api}/{request.Id}")
        ?? new(null, 400, "Não foi possível obter o jogador");

    public async Task<Response<PlayerModel?>> UpdateAsync(UpdatePlayerRequest request)
    {
        var result = await _client.PutAsJsonAsync($"{_api}/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<PlayerModel?>>()
            ?? new(null, 400, "Falha ao atualizar o jogador");
    }

    public async Task<Response<PlayerModel?>> DeleteAsync(DeletePlayerRequest request)
    {
        var result = await _client.DeleteAsync($"{_api}/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<PlayerModel?>>()
            ?? new(null, 400, "Falha ao excluir o jogador");
    }
}