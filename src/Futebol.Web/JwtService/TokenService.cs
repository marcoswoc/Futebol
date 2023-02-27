using Blazored.LocalStorage;
using Futebol.Shared.Models.User;

namespace Futebol.Web.JwtService;

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorageService;
    private const string _keyToken = "token";

    public TokenService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<TokenModel> GetTokenAsync()
    {
        return await _localStorageService.GetItemAsync<TokenModel>(_keyToken);
    }

    public async Task RemoveTokenAsync()
    {
        await _localStorageService.RemoveItemAsync(_keyToken);
    }

    public async Task SetTokenAsync(TokenModel token)
    {
        await _localStorageService.SetItemAsync(_keyToken, token);
    }
}
