using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Gender).HasMaxLength(10);
        builder.Property(p => p.Email).HasMaxLength(256);
        builder.Property(p => p.Introduction).HasMaxLength(1000);
    }
}