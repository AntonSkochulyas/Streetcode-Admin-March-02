﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class TeamMemberPositionsEntityConfigurations : IEntityTypeConfiguration<TeamMemberPositions>
    {
        public void Configure(EntityTypeBuilder<TeamMemberPositions> builder)
        {
            builder.HasKey(nameof(TeamMemberPositions.TeamMemberId), nameof(TeamMemberPositions.PositionsId));
        }
    }
}
