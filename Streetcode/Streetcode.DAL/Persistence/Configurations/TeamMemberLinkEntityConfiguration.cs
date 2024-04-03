using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Persistence.Configurations;

internal class TeamMemberLinkEntityConfiguration : IEntityTypeConfiguration<TeamMemberLink>
{
    public void Configure(EntityTypeBuilder<TeamMemberLink> builder)
    {
        builder
            .Property(s => s.TargetUrl)
            .HasMaxLength(255);
    }
}
