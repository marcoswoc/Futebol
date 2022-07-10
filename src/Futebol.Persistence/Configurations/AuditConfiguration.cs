using Futebol.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futebol.Persistence.Configurations;
public class AuditConfiguration : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.ToTable("AuditLog");
        builder.Property(p => p.UserId).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.Type).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.TableName).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.OldValues).HasColumnType("varchar").HasMaxLength(8000);
        builder.Property(p => p.NewValues).HasColumnType("varchar").HasMaxLength(8000);
        builder.Property(p => p.AffectedColumns).HasColumnType("varchar").HasMaxLength(8000);
        builder.Property(p => p.PrimaryKey).HasColumnType("varchar").HasMaxLength(8000);
    }
}
