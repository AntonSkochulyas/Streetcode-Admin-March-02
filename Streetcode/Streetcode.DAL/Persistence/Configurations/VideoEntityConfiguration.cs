using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Media;

namespace Streetcode.DAL.Persistence.Configurations;

internal class VideoEntityConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder
            .Property(s => s.Title)
            .HasMaxLength(100);
    }
}
