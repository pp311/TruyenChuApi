using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class BookCommentConfiguration : IEntityTypeConfiguration<BookComment>
{
    public void Configure(EntityTypeBuilder<BookComment> builder)
    {
        builder.HasOne(bc => bc.Book)
            .WithMany(b => b.BookComments)
            .HasForeignKey(bc => bc.BookId);
    }
}