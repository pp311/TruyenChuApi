using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Gender).HasMaxLength(10);
        builder.Property(p => p.Email).HasMaxLength(256);
        builder.Property(p => p.Introduction).HasMaxLength(1000);
        builder.HasData(
            new User
            {
                Name = "Phuc Phan",
                UserName = "pp311",
                NormalizedUserName = "PP311",
                PasswordHash = new PasswordHasher<object>().HashPassword(null, "Admin@123"),
                Email = "pp311@gmail.com",
                NormalizedEmail = "PP311@gmail.com",
                EmailConfirmed = true,
                Gender = "male",
                DateOfBirth = new DateTime(2000, 1, 1),
                Introduction = "Hello, I am Phuc Phan",
                Id = 1
            },
            new User
            {
                Name = "Jack",
                UserName = "jackie007",
                NormalizedUserName = "JACKIE007",
                PasswordHash = new PasswordHasher<object>().HashPassword(null, "Admin@123"),
                Email = "jackie007@gmail.com",
                NormalizedEmail = "JACKIE007@GMAIL.COM",
                EmailConfirmed = true,
                Gender = "male",
                DateOfBirth = new DateTime(2000, 1, 1),
                Introduction = "Hello, I am Jack",
                Id = 2
            } 
            );
    }
}