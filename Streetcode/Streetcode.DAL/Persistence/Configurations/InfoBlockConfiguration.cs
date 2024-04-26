using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.InfoBlocks;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class InfoBlockConfiguration : IEntityTypeConfiguration<InfoBlock>
    {
        public void Configure(EntityTypeBuilder<InfoBlock> builder)
        {
            builder.HasOne(ib => ib.Article)
                .WithOne(x => x.InfoBlock)
                .HasForeignKey<InfoBlock>(x => x.ArticleId);

            builder.HasOne(ib => ib.Term)
                .WithOne(x => x.InfoBlock)
                .HasForeignKey<InfoBlock>(x => x.TermId);

            builder
                .Property(s => s.VideoURL)
                .HasMaxLength(500);
        }
    }
}
