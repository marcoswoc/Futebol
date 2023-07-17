using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Futebol.Persistence.Contexts;
public class FutebolDbContextNpgsql : FutebolDbContext
{
    public FutebolDbContextNpgsql(IConfiguration configuration, IHttpContextAccessor httpContext)
        : base(httpContext, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FutebolDbContextNpgsql"), opt =>
            {

                opt.EnableRetryOnFailure();
            });
        }

        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }
}

public class FutebolDbContextNpgsqlFactory : IDesignTimeDbContextFactory<FutebolDbContextNpgsql>
{
    public FutebolDbContextNpgsql CreateDbContext(string[] args)
    {

        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var connectionString = configuration.GetConnectionString("FutebolDbContextNpgsql");
        var optionsBuilder = new DbContextOptionsBuilder<FutebolDbContextNpgsql>();
        optionsBuilder.UseNpgsql();
        var httpContext = new HttpContextAccessor();        

        return new FutebolDbContextNpgsql(configuration, httpContext);
    }
}
