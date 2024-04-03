using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Persistence.Configurations;

internal class PositionsEntityConfiguration : IEntityTypeConfiguration<Positions>
{
    public void Configure(EntityTypeBuilder<Positions> builder)
    {
        builder
            .Property(s => s.Position)
            .HasMaxLength(50);
    }
}
