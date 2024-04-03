using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Streetcode.DAL.Entities.Streetcode.TextContent;

namespace Streetcode.DAL.Persistence.Configurations;

internal class FactEntityConfiguration : IEntityTypeConfiguration<Fact>
{
    public void Configure(EntityTypeBuilder<Fact> builder)
    {
        builder.HasIndex(f => new { f.StreetcodeId, f.OrderNumber })
            .IsUnique();

        builder
            .Property(s => s.Title)
            .HasMaxLength(100);

        builder
            .Property(s => s.FactContent)
            .HasMaxLength(600);

        builder
            .Property(s => s.ImageDescription)
            .HasMaxLength(200);
    }
}
