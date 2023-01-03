namespace WebTruyenChu_Backend.Entities;

public class ChapterComment : Comment
{
    public int ChapterId { get; set; }
    
    public virtual Chapter? Chapter { get; set; }
}