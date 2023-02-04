namespace WebTruyenChu_Backend.DTOs.Book;

public class GetBookDto
{
    public int BookId { get; set; }
    public string BookName { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public string? PosterUrl { get; set; }
    public string? Slug { get; set; }
    public long ViewCount { get; set; }
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public List<GenreDto>? Genres { get; set; }
}