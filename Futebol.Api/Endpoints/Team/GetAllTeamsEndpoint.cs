
using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Team;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Team;

public class GetAllTeamsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync);

    private static async Task<IResult> HandlerAsync(
        ITeamHandler _handler,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 25)
    {
        var request = new GetAllTeamsRequest { PageNumber = pageNumber, PageSize = pageSize };

        var result = await _handler.GetAllAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
