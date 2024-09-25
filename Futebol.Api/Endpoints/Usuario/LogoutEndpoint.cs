using Microsoft.AspNetCore.Identity;

namespace Futebol.Api.Endpoints.Usuario;

internal sealed class LogoutEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/logout", HandleAsync)
        .RequireAuthorization();

    public static async Task<IResult> HandleAsync(SignInManager<IdentityUser> singInManager)
    {
        await singInManager.SignOutAsync();
        return Results.Ok();
    }
}
