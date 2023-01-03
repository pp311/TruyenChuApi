namespace WebTruyenChu_Backend.Entities;

public class BookComment : Comment
{
    public int BookId { get; set; }
    
    public virtual Book? Book { get; set; }
}