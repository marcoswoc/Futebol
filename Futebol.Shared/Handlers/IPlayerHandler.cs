using Futebol.Shared.Models;
using Futebol.Shared.Requests.Player;
using Futebol.Shared.Responses;

namespace Futebol.Shared.Handlers;
public interface IPlayerHandler
{
    Task<Response<PlayerModel?>> CreateAsync(CreatePlayerRequest request);
    Task<PagedResponse<List<PlayerModel>?>> GetAllAsync(GetAllPlayersRequest request);
    Task<Response<PlayerModel?>> GetByIdAsync(GetByIdPlayerRequest request);
    Task<Response<PlayerModel?>> UpdateAsync(UpdatePlayerRequest request);
    Task<Response<PlayerModel?>> DeleteAsync(DeletePlayerRequest request);
}