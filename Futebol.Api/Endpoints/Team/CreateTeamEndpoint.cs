using Futebol.Shared.Handlers;
using Futebol.Shared.Requests.Team;

namespace Futebol.Api.Endpoints.Team;

public class CreateTeamEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync);
    private static async Task<IResult> HandleAsync(
        ITeamHandler _handler,
        CreateTeamRequest request)
    {
        var result = await _handler.CreateAsync(request);

        return result.IsSucess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}