using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Player;

namespace Futebol.Api.Endpoints.Player;

public class CreatePlayerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);

    private static async Task<IResult> HandleAsync(
        IPlayerHandler _handler,
        CreatePlayerRequest request)
    {
        var result = await _handler.CreateAsync(request);

        return result.IsSucess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
