using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class AuthorShipConfiguration : IEntityTypeConfiguration<AuthorShip>
    {
        public void Configure(EntityTypeBuilder<AuthorShip> builder)
        {
            builder.HasMany(a => a.InfoBlocks)
                 .WithOne(ib => ib.AuthorShip)
                 .HasForeignKey(a => a.AuthorShipId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(a => a.Text)
                .HasMaxLength(500);

            builder.HasMany(a => a.AuthorShipHyperLinks)
                .WithOne(a => a.AuthorShip)
                .HasForeignKey(a => a.AuthorShipId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
