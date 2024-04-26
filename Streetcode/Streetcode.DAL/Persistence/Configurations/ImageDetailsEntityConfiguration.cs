using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.DAL.Persistence.Configurations;

internal class ImageDetailsEntityConfiguration : IEntityTypeConfiguration<ImageDetails>
{
    public void Configure(EntityTypeBuilder<ImageDetails> builder)
    {
        builder
            .Property(s => s.Title)
            .HasMaxLength(100);

        builder
            .Property(s => s.Alt)
            .HasMaxLength(300);
    }
}
