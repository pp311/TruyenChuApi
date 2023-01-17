using System.ComponentModel.DataAnnotations;
using WebTruyenChu_Backend.Constants;

namespace WebTruyenChu_Backend.DTOs;

public class UpdateBookDto
{
    public int? BookId { get; set; }
    [MaxLength(256)]
    [Required]
    public string? BookName { get; set; }
    public string? Description { get; set; }
    [MaxLength(50)] 
    public string? Status { get; set; }
    public string? PosterUrl { get; set; }
    
    public int? AuthorId { get; set; }

    public List<int> GenreIds { get; set; } = new List<int>();
}