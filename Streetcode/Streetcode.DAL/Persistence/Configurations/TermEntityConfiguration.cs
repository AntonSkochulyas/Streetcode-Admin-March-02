using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Streetcode.TextContent;

namespace Streetcode.DAL.Persistence.Configurations;

internal class TermEntityConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder
            .Property(s => s.Description)
            .HasMaxLength(500);

        builder
            .Property(s => s.Title)
            .HasMaxLength(50);
    }
}
