using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Player;
using Microsoft.AspNetCore.Mvc;

namespace Futebol.Api.Endpoints.Player;

public class GetAllPlayersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync);

    private static async Task<IResult> HandlerAsync(
        IPlayerHandler _handler,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 25)
    {
        var request = new GetAllPlayersRequest { PageNumber = pageNumber, PageSize = pageSize };

        var result = await _handler.GetAllAsync(request);

        return result.IsSucess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
