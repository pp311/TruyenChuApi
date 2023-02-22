namespace WebTruyenChu_Backend.DTOs.Comment;

public class AddBookCommentDto
{
    public int UserId { get; set; }
    public int? ParentId { get; set; }
    public int BookId { get; set; }

    public string Content { get; set; } = null!;
}