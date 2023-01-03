using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre> 
{
    public void Configure(EntityTypeBuilder<BookGenre> builder)
    {
        builder.HasKey(p => new { p.BookId, p.GenreId });

        builder.HasOne(bg => bg.Book)
            .WithMany(b => b.BookGenres)
            .HasForeignKey(bg => bg.BookId);

        builder.HasOne(bg => bg.Genre)
            .WithMany(g => g.BookGenres)
            .HasForeignKey(bg => bg.GenreId);
    }
}