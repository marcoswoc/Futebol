using Castle.Core.Configuration;
using Futebol.Domain.Entity;
using Futebol.Persistence.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Futebol.Persistence.Contexts;
public partial class FutebolDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public IHttpContextAccessor HttpContext { get; }

    public FutebolDbContext( IHttpContextAccessor httpContext)
        : base()
    {
        HttpContext = httpContext;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasSequence<int>("RoundSequence").StartsAt(1).IncrementsBy(1);
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(FutebolDbContext).Assembly);
    }

    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Round> Rounds { get; set; }
    public virtual DbSet<Vote> Votes { get; set; }
    public virtual DbSet<Audit> AuditLogs { get; set; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var handler = new ContextEventHandler();
        handler.InvokeSavingChanges(this);

        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        handler.InvokeSavedChanges(this);

        return result;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var handler = new ContextEventHandler();
        handler.InvokeSavingChanges(this);

        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        handler.InvokeSavedChanges(this);

        return result;
    }

}
