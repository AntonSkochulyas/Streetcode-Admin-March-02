using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.DAL.Persistence.Configurations;

internal class PartnerSourceLinkEntityConfiguration : IEntityTypeConfiguration<PartnerSourceLink>
{
    public void Configure(EntityTypeBuilder<PartnerSourceLink> builder)
    {
        builder
            .Property(s => s.TargetUrl)
            .HasMaxLength(255);
    }
}
