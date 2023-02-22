namespace WebTruyenChu_Backend.DTOs.Comment;

public class GetCommentDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Avatar { get; set; }
    public int CommentId { get; set; }
    public int ParentId { get; set; }

    public string Content { get; set; } = null!;
    public int LikeCount { get; set; }
    
    public DateTime? Time { get; set; }
}