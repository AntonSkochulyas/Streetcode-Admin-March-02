using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Media;

namespace Streetcode.DAL.Persistence.Configurations;

internal class AudioEntityConfiguration : IEntityTypeConfiguration<Audio>
{
    public void Configure(EntityTypeBuilder<Audio> builder)
    {
        builder
            .Property(s => s.Title)
            .HasMaxLength(100);

        builder
            .Property(s => s.MimeType)
            .HasMaxLength(10);
    }
}
