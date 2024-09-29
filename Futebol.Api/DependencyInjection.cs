namespace Futebol.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
        => services
        .AddCrossOrigin()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();            

    public static IServiceCollection AddCrossOrigin(this IServiceCollection services)
    {
        services.AddCors(
            options => options.AddPolicy(
                "futebolApi",
                policy => policy
                    .WithOrigins(
                        "http://localhost:5029",
                        "https://localhost:7283"
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                ));

        return services;
    }
}
