using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class ImageMainEntityConfiguration : IEntityTypeConfiguration<ImageMain>
    {
        public void Configure(EntityTypeBuilder<ImageMain> builder)
        {
            builder
                .Property(im => im.MimeType)
                .HasMaxLength(10);
        }
    }
}
