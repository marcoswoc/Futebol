using Futebol.Shared.Models;
using Futebol.Shared.Requests.Player;
using Futebol.Shared.Responses;

namespace Futebol.Shared.Handlers;
public interface IPlayerHandler
{
    Task<Response<PlayerModel?>> CreateAsync(CreatePlayerRequest request);
}