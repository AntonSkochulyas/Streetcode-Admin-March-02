using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Persistence.Configurations;

internal class UserEntityConfiguration : IEntityTypeConfiguration<UserAdditionalInfo>
{
    public void Configure(EntityTypeBuilder<UserAdditionalInfo> builder)
    {
        builder
            .Property(s => s.FirstName)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(s => s.SecondName)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(s => s.ThirdName)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(s => s.Email)
            .HasMaxLength(32)
            .IsRequired();

        builder
            .Property(s => s.Age)
            .IsRequired();

        builder
            .Property(s => s.Phone)
            .HasMaxLength(13)
            .IsRequired();
    }
}
