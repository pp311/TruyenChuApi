namespace WebTruyenChu_Backend.DTOs;

public class BookInsertDto
{
    public string BookName { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public string? PosterUrl { get; set; }
    public string? Slug { get; set; }
    public long ViewCount { get; set; }
    
    public int AuthorId { get; set; }
}