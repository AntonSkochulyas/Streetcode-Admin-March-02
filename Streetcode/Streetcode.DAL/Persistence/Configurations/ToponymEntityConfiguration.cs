using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Toponyms;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class ToponymEntityConfiguration : IEntityTypeConfiguration<Toponym>
    {
        public void Configure(EntityTypeBuilder<Toponym> builder)
        {
            builder.HasOne(d => d.Coordinate)
                .WithOne(p => p.Toponym)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(s => s.Oblast)
                .HasMaxLength(100);

            builder
                .Property(s => s.AdminRegionOld)
                .HasMaxLength(150);

            builder
                .Property(s => s.AdminRegionNew)
                .HasMaxLength(150);

            builder
                .Property(s => s.Gromada)
                .HasMaxLength(150);

            builder
                .Property(s => s.Community)
                .HasMaxLength(150);

            builder
                .Property(s => s.StreetName)
                .HasMaxLength(150);

            builder
                .Property(s => s.StreetType)
                .HasMaxLength(50);
        }
    }
}
