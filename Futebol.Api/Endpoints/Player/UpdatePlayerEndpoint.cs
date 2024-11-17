using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Player;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Player;

public class UpdatePlayerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync);

    private static async Task<IResult> HandlerAsync(
        IPlayerHandler _handler,
        [FromBody] UpdatePlayerRequest request,
        [FromRoute] Guid id)
    {
        request.Id = id;
        var result = await _handler.UpdateAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}