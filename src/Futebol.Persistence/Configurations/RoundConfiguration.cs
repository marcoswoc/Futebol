using Futebol.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futebol.Persistence.Configurations;
public class RoundConfiguration : IEntityTypeConfiguration<Round>
{
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ToTable("Rounds");

        builder.Property(p => p.Active).HasColumnName("bit");

        builder.HasIndex(p => p.DeletedAt);
        builder.HasQueryFilter(m => m.DeletedAt == null);
    }
}
