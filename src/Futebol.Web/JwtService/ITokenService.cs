using Futebol.Shared.Models.User;

namespace Futebol.Web.JwtService;

public interface ITokenService
{
    Task<TokenModel> GetTokenAsync();
    Task RemoveTokenAsync();
    Task SetTokenAsync(TokenModel token);
}
