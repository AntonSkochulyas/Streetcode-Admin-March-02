using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.AdditionalContent;

namespace Streetcode.DAL.Persistence.Configurations;

internal class SubtitleEntityConfiguration : IEntityTypeConfiguration<Subtitle>
{
    public void Configure(EntityTypeBuilder<Subtitle> builder)
    {
        builder
            .Property(s => s.SubtitleText)
            .HasMaxLength(500);
    }
}
