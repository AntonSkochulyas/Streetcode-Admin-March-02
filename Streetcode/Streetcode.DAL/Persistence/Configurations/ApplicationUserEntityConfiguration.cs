using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Persistence.Configurations;

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(d => d.RefreshTokens)
            .WithOne(p => p.ApplicationUser)
            .HasForeignKey(d => d.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
