using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class ReadingHistoryConfiguration : IEntityTypeConfiguration<ReadingHistory>
{
    public void Configure(EntityTypeBuilder<ReadingHistory> builder)
    {
        builder.HasKey(p => new { p.ChapterId, p.UserId });
        
        builder.HasOne(rh => rh.Chapter)
            .WithMany(c => c.ReadingHistory)
            .HasForeignKey(rh => rh.ChapterId);

        builder.HasOne(rh => rh.User)
            .WithMany(u => u.ReadingHistory)
            .HasForeignKey(rh => rh.UserId);
    }
}