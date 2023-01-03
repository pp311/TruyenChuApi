using Microsoft.AspNetCore.Identity;

namespace WebTruyenChu_Backend.Entities;
public class User : IdentityUser<int>
{
    public string? Avatar { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Introduction { get; set; }

    public virtual ICollection<ReadingHistory>? ReadingHistory { get; set; }
    public virtual ICollection<SavedBook>? SavedBooks { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<CommentReaction>? CommentsReactions { get; set; }
}