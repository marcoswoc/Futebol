using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Futebol.Persistence.Contexts;
public class FutebolDbContextNpgsql : FutebolDbContext
{
    public IConfiguration Configuration { get; set; }

    public FutebolDbContextNpgsql(IConfiguration configuration)
        : base()
    {
        Configuration = configuration;
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

        return new FutebolDbContextNpgsql(configuration);
    }
}
