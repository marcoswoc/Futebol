using Futebol.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futebol.Persistence.Configurations;
public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Players");

        builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(100);
        builder.Property(p => p.ImageUrl).HasColumnType("varchar").HasMaxLength(200);

        builder.HasIndex(p => p.DeletedAt);
        builder.HasQueryFilter(m => m.DeletedAt == null);
    }
}
