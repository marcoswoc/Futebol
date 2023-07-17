using Futebol.Domain.Entity;
using Futebol.Infrastructure.Enums;
using Futebol.Persistence.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Futebol.Persistence.Contexts;
public partial class FutebolDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public IHttpContextAccessor  HttpContext { get; }
    public IConfiguration Configuration { get; }

    public FutebolDbContext(IHttpContextAccessor httpContext, IConfiguration configuration)
        : base()
    {
        HttpContext = httpContext;
        Configuration = configuration;
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

        BeforeSaveChanges().ConfigureAwait(false).GetAwaiter().GetResult();

        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        handler.InvokeSavedChanges(this);

        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var handler = new ContextEventHandler();
        handler.InvokeSavingChanges(this);

        await BeforeSaveChanges();

        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        handler.InvokeSavedChanges(this);

        return result;
    }

    private async Task BeforeSaveChanges()
    {
        if (Convert.ToBoolean(Configuration["AuditConfig"]) is false)
            return;

        var login = HttpContext?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries())
        {

            if (entry.Entity is Audit || entry.State is EntityState.Detached or EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry(entry) { TableName = entry.Entity.GetType().BaseType.Name, UserId = login };

            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = AuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }                
            }
            await AuditLogs.AddAsync(auditEntry.ToAudit());
        }
    }

}
