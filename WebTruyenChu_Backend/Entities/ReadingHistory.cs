namespace WebTruyenChu_Backend.Entities;
public class ReadingHistory : AuditableEntity
{
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    
    public int ChapterId { get; set; }
    public virtual Chapter? Chapter { get; set; }
}