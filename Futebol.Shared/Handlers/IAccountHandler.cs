using Futebol.Shared.Requests.Account;
using Futebol.Shared.Responses;

namespace Futebol.Shared.Handlers;
public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
