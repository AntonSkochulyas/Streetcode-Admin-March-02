using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class AuthorShipHyperLinkConfiguration : IEntityTypeConfiguration<AuthorShipHyperLink>
    {
        public void Configure(EntityTypeBuilder<AuthorShipHyperLink> builder)
        {
            builder
                .Property(s => s.Title)
                .HasMaxLength(150);
        }
    }
}
