using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.BookName).IsRequired().HasMaxLength(256);
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.Slug).HasMaxLength(256);
        builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
        builder.Property(p => p.PosterUrl).HasMaxLength(256);
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
    }
}