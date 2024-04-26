using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.InfoBlocks.Articles;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                .Property(s => s.Title)
                .HasMaxLength(50);

            builder
                .Property(s => s.Text)
                .HasMaxLength(15000);
        }
    }
}
