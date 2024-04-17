using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Analytics;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class StatisticRecordEntityConfiguration : IEntityTypeConfiguration<StatisticRecord>
    {
        public void Configure(EntityTypeBuilder<StatisticRecord> builder)
        {
            //builder.HasOne(sr => sr.StreetcodeCoordinate)
            //     .WithOne(sc => sc.StatisticRecord)
            //     .HasForeignKey<StatisticRecord>(sr => sr.StreetcodeCoordinateId);

            builder
                .Property(s => s.Address)
                .HasMaxLength(150);
        }
    }
}
