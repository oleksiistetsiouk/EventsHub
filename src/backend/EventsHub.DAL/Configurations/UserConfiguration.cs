﻿using EventsHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsHub.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(e => e.UserId).ValueGeneratedOnAdd();
            builder.Property(e => e.Email).HasMaxLength(256);
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
