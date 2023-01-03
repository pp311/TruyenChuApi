namespace WebTruyenChu_Backend.Entities;
public class SavedBook : AuditableEntity
{
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    
    public int BookId { get; set; }
    public virtual Book? Book { get; set; }
}