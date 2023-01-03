using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class SavedBookConfiguration : IEntityTypeConfiguration<SavedBook>
{
    public void Configure(EntityTypeBuilder<SavedBook> builder)
    {
        builder.HasKey(p => new { p.BookId, p.UserId });
        
        builder.HasOne(sb => sb.Book)
            .WithMany(b => b.SavedBook)
            .HasForeignKey(sb => sb.BookId);

        builder.HasOne(sb => sb.User)
            .WithMany(u => u.SavedBooks)
            .HasForeignKey(sb => sb.UserId);
    }
}