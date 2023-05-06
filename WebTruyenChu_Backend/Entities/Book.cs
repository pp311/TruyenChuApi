namespace WebTruyenChu_Backend.Entities;

public class Book : AuditableEntity
{
    public Book()
    {
        SavedBook = new List<SavedBook>();
        Chapters = new List<Chapter>();
        BookComments = new List<BookComment>();
        BookGenres = new List<BookGenre>();
        Reviews = new List<Review>();
    }
    public int BookId { get; set; }
    public string BookName { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public string? PosterUrl { get; set; }
    public string? Slug { get; set; }
    public long ViewCount { get; set; }
    public int AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<SavedBook> SavedBook { get; set; }
    public virtual ICollection<Chapter> Chapters { get; set; }
    public virtual ICollection<BookGenre> BookGenres { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<BookComment> BookComments { get; set; }
}
