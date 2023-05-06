namespace WebTruyenChu_Backend.DTOs.Chapter;

public class GetChapterDetailDto
{
    public int ChapterId { get; set; }
    public int BookId { get; set; }
    public string? BookName { get; set; }
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public string? ChapterName { get; set; }
    public string? Content { get; set; }
    public int ChapterIndex { get; set;}
    public int PreviousChapterId { get; set; }
    public int NextChapterId { get; set; }
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
