namespace WebTruyenChu_Backend.Entities;
public class Comment : AuditableEntity
{
    public int CommentId { get; set; }
    public int? ParentId { get; set; }
    public string? Content { get; set; }
    
    public int? UserId { get; set; } 
    
    public virtual User? User { get; set; }
    
    public virtual ICollection<CommentReaction>? CommentsReactions { get; set; }

}