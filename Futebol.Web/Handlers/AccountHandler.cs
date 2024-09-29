using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Account;
using Futebol.Shared.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Futebol.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient("futebol");

    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return result.IsSuccessStatusCode
            ? new("Login realizado com sucesso", 200, "Login realizado com sucesso")
            : new(null, 400, "Não foi possível realizar login");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");

        await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
    }
}