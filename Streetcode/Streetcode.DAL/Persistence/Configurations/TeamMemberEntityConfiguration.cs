using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class TeamMemberEntityConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasOne(x => x.Image)
               .WithOne(x => x.TeamMember)
               .HasForeignKey<TeamMember>(x => x.ImageId);

            builder.HasMany(x => x.Positions)
                .WithMany(x => x.TeamMembers)
                .UsingEntity<TeamMemberPositions>(
                    tp => tp.HasOne(x => x.Positions).WithMany().HasForeignKey(x => x.PositionsId),
                    tp => tp.HasOne(x => x.TeamMember).WithMany().HasForeignKey(x => x.TeamMemberId));

            builder.HasMany(x => x.TeamMemberLinks)
                .WithOne(x => x.TeamMember)
                .HasForeignKey(x => x.TeamMemberId);

            builder
                .Property(s => s.FirstName)
                .HasMaxLength(50);

            builder
                .Property(s => s.LastName)
                .HasMaxLength(50);

            builder
                .Property(s => s.Description)
                .HasMaxLength(150);
        }
    }
}
