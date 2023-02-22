namespace WebTruyenChu_Backend.DTOs.Comment;

public class AddChapterCommentDto
{
    public int UserId { get; set; }
    public int? ParentId { get; set; }
    public int ChapterId { get; set; }

    public string Content { get; set; } = null!;
}