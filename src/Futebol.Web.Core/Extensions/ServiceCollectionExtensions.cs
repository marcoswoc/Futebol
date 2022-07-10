using Futebol.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futebol.Web.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddFutebolCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", builder => {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddFutebolServices(configuration);
        services.AddFutebolRepositories(configuration);
        services.AddIdentity(configuration);
        services.AddSwagger(configuration);
        services.AddSeed(configuration);
        services.AddAutoMapper(x => { x.AllowNullCollections = true; }, typeof(MapperProfile));
    }
}
