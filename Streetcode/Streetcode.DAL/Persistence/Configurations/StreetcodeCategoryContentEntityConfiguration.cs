using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Sources;

namespace Streetcode.DAL.Persistence.Configurations;

internal class StreetcodeCategoryContentEntityConfiguration : IEntityTypeConfiguration<StreetcodeCategoryContent>
{
    public void Configure(EntityTypeBuilder<StreetcodeCategoryContent> builder)
    {
        builder
            .Property(s => s.Text)
            .HasMaxLength(1000);
    }
}
