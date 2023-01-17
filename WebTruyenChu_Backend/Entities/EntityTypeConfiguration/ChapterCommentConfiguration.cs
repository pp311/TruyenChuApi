using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class ChapterCommentConfiguration : IEntityTypeConfiguration<ChapterComment>
{
    public void Configure(EntityTypeBuilder<ChapterComment> builder)
    {
        builder.HasOne(cc => cc.Chapter)
            .WithMany(c => c.ChapterComments)
            .HasForeignKey(cc => cc.ChapterId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}