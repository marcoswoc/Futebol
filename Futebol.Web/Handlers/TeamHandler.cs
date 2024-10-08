﻿using Futebol.Shared.Handlers;
using Futebol.Shared.Models;
using Futebol.Shared.Requests.Team;
using Futebol.Shared.Responses;
using System.Net.Http.Json;

namespace Futebol.Web.Handlers;

public class TeamHandler(IHttpClientFactory httpClientFactory) : ITeamHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient("futebol");
    private const string _api = "v1/teams";

    public async Task<Response<TeamModel?>> CreateAsync(CreateTeamRequest request)
    {
        var result = await _client.PostAsJsonAsync(_api, request);
        return await result.Content.ReadFromJsonAsync<Response<TeamModel?>>()
            ?? new(null, 400, "Falha ao criar o time");
    }

    public async Task<PagedResponse<List<TeamModel>?>> GetAllAsync(GetAllTeamsRequest request)
    => await _client.GetFromJsonAsync<PagedResponse<List<TeamModel>?>>(_api)
        ?? new(null, 400, "Não foi possível obter os times");
}
