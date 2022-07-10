using Futebol.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futebol.Persistence.Configurations;
public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("Votes");

        builder.HasIndex(p => p.DeletedAt);
        builder.HasQueryFilter(m => m.DeletedAt == null);
    }
}
