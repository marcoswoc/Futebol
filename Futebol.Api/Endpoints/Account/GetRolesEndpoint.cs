using Futebol.Shared.Models.Account;
using System.Security.Claims;

namespace Futebol.Api.Endpoints.Account;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/roles", Handle)
        .RequireAuthorization();

    public static Task<IResult> Handle(ClaimsPrincipal user)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());

        var identity = (ClaimsIdentity)user.Identity;

        var roles = identity
        .FindAll(identity.RoleClaimType)
        .Select(c => new RoleClaimModel
        {
            Issuer = c.Issuer,
            OriginalIssuer = c.OriginalIssuer,
            Type = c.Type,
            Value = c.Value,
            ValueType = c.ValueType
        });

        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}
