using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Player;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Player;

public class GetByIdPlayerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync);

    private static async Task<IResult> HandlerAsync(
        IPlayerHandler _handler,
        [FromRoute] Guid id)
    {
        var request = new GetByIdPlayerRequest { Id = id };
        var result = await _handler.GetByIdAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}