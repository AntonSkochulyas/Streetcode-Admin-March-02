using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class PartnerEntityConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.HasMany(d => d.PartnerSourceLinks)
                .WithOne(p => p.Partner)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.IsKeyPartner)
                .HasDefaultValue("false");

            builder
                .Property(s => s.Title)
                .HasMaxLength(255);

            builder
                .Property(s => s.UrlTitle)
                .HasMaxLength(255);
        }
    }
}
