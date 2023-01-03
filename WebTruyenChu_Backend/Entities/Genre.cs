namespace WebTruyenChu_Backend.Entities;
public class Genre : AuditableEntity
{
    public int GenreId { get; set; }
    public string? GenreName { get; set; }
    
    public virtual ICollection<BookGenre>? BookGenres { get; set; }
}