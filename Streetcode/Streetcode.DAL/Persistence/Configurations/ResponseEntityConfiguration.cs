using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Feedback;

namespace Streetcode.DAL.Persistence.Configurations;

internal class ResponseEntityConfiguration : IEntityTypeConfiguration<Response>
{
    public void Configure(EntityTypeBuilder<Response> builder)
    {
        builder
            .Property(s => s.Name)
            .HasMaxLength(50);

        builder
            .Property(s => s.Email)
            .HasMaxLength(50);

        builder
            .Property(s => s.Description)
            .HasMaxLength(1000);
    }
}
