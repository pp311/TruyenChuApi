namespace WebTruyenChu_Backend.DTOs;

public class BookOverviewDto
{
    public int BookId { get; set; }
    public string? BookName { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public string? Slug { get; set; }
    
    public string? AuthorName { get; set; }
    public List<GenreDto>? Genres { get; set; }
}