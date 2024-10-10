
using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Team;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Team;

public class UpdateTeamEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync);

    private static async Task<IResult> HandleAsync(
        ITeamHandler _handler,
        [FromBody] UpdateTeamRequest request,
        [FromRoute] Guid id)
    {
        request.Id = id;
        var result = await _handler.UpdateAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
