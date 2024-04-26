using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Streetcode.TextContent;

namespace Streetcode.DAL.Persistence.Configurations;

internal class TextEntityConfiguration : IEntityTypeConfiguration<Text>
{
    public void Configure(EntityTypeBuilder<Text> builder)
    {
        builder
            .Property(s => s.TextContent)
            .HasMaxLength(15000);

        builder
            .Property(s => s.Title)
            .HasMaxLength(300);

        builder
            .Property(s => s.AdditionalText)
            .HasMaxLength(500);
    }
}
