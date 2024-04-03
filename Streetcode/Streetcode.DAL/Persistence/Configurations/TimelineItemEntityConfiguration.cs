using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Timeline;

namespace Streetcode.DAL.Persistence.Configurations;

internal class TimelineItemEntityConfiguration : IEntityTypeConfiguration<TimelineItem>
{
    public void Configure(EntityTypeBuilder<TimelineItem> builder)
    {
        builder
            .Property(s => s.Description)
            .HasMaxLength(600);

        builder
            .Property(s => s.Title)
            .HasMaxLength(100);
    }
}
