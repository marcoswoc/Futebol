using Microsoft.Extensions.DependencyInjection;

namespace Futebol.Web.Core.Extensions;
public static class CorsPolicyFutebolExtensions
{
    private const string _name = "CorsPolicy";
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(_name, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}
