using Futebol.Web.JwtService;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Futebol.Web.AuthenticationProvider;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService _tokenService;

    public CustomAuthenticationStateProvider(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public void StateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenDTO = await _tokenService.GetTokenAsync();
        var identity = string.IsNullOrEmpty(tokenDTO?.Token) || tokenDTO?.ValidTo < DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(tokenDTO.Token), "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
