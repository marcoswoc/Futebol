using Futebol.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futebol.Persistence.Configurations;
public class RoundConfiguration : IEntityTypeConfiguration<Round>
{
    private const string _nextValue = "nextval('\"RoundSequence\"')";
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ToTable("Rounds");

        builder.Property(p => p.Active);
        builder.Property(p => p.Number).HasDefaultValueSql(_nextValue);

        builder.HasIndex(p => p.DeletedAt);
        builder.HasQueryFilter(m => m.DeletedAt == null);
    }
}
