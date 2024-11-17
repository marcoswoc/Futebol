using Futebol.Shared.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Player;

public class DeletePlayerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync);

    private static async Task<IResult> HandlerAsync(
        IPlayerHandler _handler,
        [FromRoute] Guid id)
    {
        var result = await _handler.DeleteAsync(new() { Id = id });

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}