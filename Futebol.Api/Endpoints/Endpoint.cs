using Futebol.Api.Endpoints.Account;
using Futebol.Api.Endpoints.Player;
using Futebol.Api.Endpoints.Team;
using Futebol.Api.Entities.Account;

namespace Futebol.Api.Endpoints;

internal static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        endpoints.MapGroup("v1/teams")
            .WithTags("teams")
            .RequireAuthorization()
            .MapEndpoint<CreateTeamEndpoint>()
            .MapEndpoint<GetAllTeamsEndpoint>()
            .MapEndpoint<GetByIdTeamEndpoint>()
            .MapEndpoint<UpdateTeamEndpoint>()
            .MapEndpoint<DeleteTeamEndpoint>();

        endpoints.MapGroup("v1/players")
            .WithTags("players")
            .RequireAuthorization()
            .MapEndpoint<CreatePlayerEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
