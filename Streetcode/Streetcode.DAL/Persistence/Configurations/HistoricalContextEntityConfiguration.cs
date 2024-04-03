using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Timeline;

namespace Streetcode.DAL.Persistence.Configurations;

internal class HistoricalContextEntityConfiguration : IEntityTypeConfiguration<HistoricalContext>
{
    public void Configure(EntityTypeBuilder<HistoricalContext> builder)
    {
        builder
            .Property(s => s.Title)
            .HasMaxLength(50);
    }
}
