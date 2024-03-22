﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streetcode.DAL.Entities.News;

namespace Streetcode.DAL.Persistence.Configurations
{
    internal class NewsEntityConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder
                .HasOne(x => x.Image)
                .WithOne(x => x.News)
                .HasForeignKey<News>(x => x.ImageId);
        }
    }
}
