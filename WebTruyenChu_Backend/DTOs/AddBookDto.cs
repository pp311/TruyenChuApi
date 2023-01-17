using System.ComponentModel.DataAnnotations;
using WebTruyenChu_Backend.Constants;

namespace WebTruyenChu_Backend.DTOs;

public class AddBookDto
{
    [MaxLength(256)]
    [Required]
    public string? BookName { get; set; }
    public string Description { get; set; } = "";
    [MaxLength(50)] 
    public string Status { get; set; } = BookStatus.OnGoing;
    public string? PosterUrl { get; set; }
    public string? Slug { get; set; }
    public long ViewCount { get; set; } = 0;
    
    public int AuthorId { get; set; }

    public List<int> GenreIds { get; set; } = new List<int>();
}