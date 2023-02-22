namespace WebTruyenChu_Backend.DTOs.Comment;

public class UpdateCommentDto
{ 
    [System.Text.Json.Serialization.JsonIgnore] 
    public int CommentId {get; set; }
    public string Content { get; set; } = null!;
}