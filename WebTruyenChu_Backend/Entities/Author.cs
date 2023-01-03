namespace WebTruyenChu_Backend.Entities;

public class Author : AuditableEntity
{
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = null!;
}