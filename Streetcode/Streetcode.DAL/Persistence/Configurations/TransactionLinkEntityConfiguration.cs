using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Transactions;

namespace Streetcode.DAL.Persistence.Configurations;

internal class TransactionLinkEntityConfiguration : IEntityTypeConfiguration<TransactionLink>
{
    public void Configure(EntityTypeBuilder<TransactionLink> builder)
    {
        builder
            .Property(s => s.UrlTitle)
            .HasMaxLength(255);

        builder
            .Property(s => s.Url)
            .HasMaxLength(255);
    }
}
