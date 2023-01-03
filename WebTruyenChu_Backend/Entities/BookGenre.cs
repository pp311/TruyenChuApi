namespace WebTruyenChu_Backend.Entities;

public class BookGenre : AuditableEntity
{
    public int BookId { get; set; }
    public virtual Book? Book { get; set; }
    
    public int GenreId { get; set; }
    public virtual Genre? Genre { get; set; }
}