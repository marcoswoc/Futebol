using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Team;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Team;

public class GetByIdTeamEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        ITeamHandler _handler,
        [FromRoute] Guid id)
    {
        var request = new GetByIdTeamRequest { Id = id };
        var result = await _handler.GetByIdAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

}
