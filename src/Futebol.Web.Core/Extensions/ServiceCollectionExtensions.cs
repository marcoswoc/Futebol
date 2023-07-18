using Futebol.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futebol.Web.Core.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddFutebolCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddCorsPolicy();
        services.AddFutebolServices();
        services.AddFutebolRepositories();
        services.AddIdentity(configuration);
        services.AddSwagger();
        services.AddSeed();
        services.AddAutoMapper(x => { x.AllowNullCollections = true; }, typeof(MapperProfile));
    }
}
