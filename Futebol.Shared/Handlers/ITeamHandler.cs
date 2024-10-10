using Futebol.Shared.Models;
using Futebol.Shared.Requests.Team;
using Futebol.Shared.Responses;

namespace Futebol.Shared.Handlers;
public interface ITeamHandler
{
    Task<Response<TeamModel?>> CreateAsync(CreateTeamRequest request);
    Task<PagedResponse<List<TeamModel>?>> GetAllAsync(GetAllTeamsRequest request);
    Task<Response<TeamModel?>> GetByIdAsync(GetByIdTeamRequest request);
    Task<Response<TeamModel?>> UpdateAsync(UpdateTeamRequest request);
}
