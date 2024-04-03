using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Persistence.Configurations;

internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(s => s.Name)
            .HasMaxLength(50);

        builder
            .Property(s => s.Surname)
            .HasMaxLength(50);

        builder
            .Property(s => s.Surname)
            .HasMaxLength(50);

        builder
            .Property(s => s.Login)
            .HasMaxLength(20);

        builder
            .Property(s => s.Password)
            .HasMaxLength(20);
    }
}
