namespace WebTruyenChu_Backend.Entities;
public class CommentReaction : AuditableEntity
{
    public string? ReactionType { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    
    public int CommentId { get; set; }
    public virtual Comment? Comment { get; set; }
}