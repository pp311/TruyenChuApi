using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.Property(p => p.Content).IsRequired();
        builder.Property(p => p.ChapterName).IsRequired().HasMaxLength(256);

        builder.HasOne(c => c.Book)
            .WithMany(b => b.Chapters)
            .HasForeignKey(c => c.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}