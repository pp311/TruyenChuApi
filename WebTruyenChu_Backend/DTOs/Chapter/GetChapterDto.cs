namespace WebTruyenChu_Backend.DTOs.Chapter;

public class GetChapterDto
{
    public int ChapterId { get; set; }
    public int BookId { get; set; }
    public string? ChapterName { get; set; } 
    public string? Content { get; set; }
    public int WordCount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool ShouldSerializeContent()
    {
        return Content is not null;
    }
    public bool ShouldSerializeCreatedAt()
    {
        return CreatedAt is not null;
    }
    public bool ShouldSerializeModifiedAt()
    {
        return ModifiedAt is not null;
    }
}