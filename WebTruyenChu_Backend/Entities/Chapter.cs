namespace WebTruyenChu_Backend.Entities;

public class Chapter : AuditableEntity 
{
    public Chapter()
    {
        ReadingHistory = new List<ReadingHistory>();
        ChapterComments = new List<ChapterComment>();
    }
    public int ChapterId { get; set; }
    public string? ChapterName { get; set; } 
    //public int ChapterIndex { get; set; }
    public string? Content { get; set; }
    public int WordCount { get; set; }
    public int BookId { get; set; }
   
    public virtual ICollection<ReadingHistory> ReadingHistory { get; set; }
    public virtual ICollection<ChapterComment> ChapterComments { get; set; }
    public virtual Book? Book { get; set; }
}