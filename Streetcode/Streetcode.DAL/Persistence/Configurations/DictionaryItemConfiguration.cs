using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Dictionaries;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class DictionaryItemConfiguration : IEntityTypeConfiguration<DictionaryItem>
    {
        public void Configure(EntityTypeBuilder<DictionaryItem> builder)
        {
            builder
                .Property(s => s.Word)
                .HasMaxLength(50);

            builder
                .Property(s => s.Description)
                .HasMaxLength(500);
        }
    }
}
