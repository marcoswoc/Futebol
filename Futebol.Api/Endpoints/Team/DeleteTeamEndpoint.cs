
using Futebol.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Team;

public class DeleteTeamEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync);

    private static async Task<IResult> HandlerAsync(
        ITeamHandler _handler,
        [FromRoute] Guid id)
    {
        var result = await _handler.DeleteAsync(new() { Id = id });

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
