using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.DAL.Persistence.Configurations;

internal class ArtEntityConfiguration : IEntityTypeConfiguration<Art>
{
    public void Configure(EntityTypeBuilder<Art> builder)
    {
        builder
            .Property(s => s.Description)
            .HasMaxLength(400);

        builder
            .Property(s => s.Title)
            .HasMaxLength(150);
    }
}
