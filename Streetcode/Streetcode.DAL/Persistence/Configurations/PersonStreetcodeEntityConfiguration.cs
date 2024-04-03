using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Streetcode.Types;

namespace Streetcode.DAL.Persistence.Configurations;

internal class PersonStreetcodeEntityConfiguration : IEntityTypeConfiguration<PersonStreetcode>
{
    public void Configure(EntityTypeBuilder<PersonStreetcode> builder)
    {
        builder
            .Property(s => s.FirstName)
            .HasMaxLength(50);

        builder
            .Property(s => s.Rank)
            .HasMaxLength(50);

        builder
            .Property(s => s.LastName)
            .HasMaxLength(50);
    }
}
