using WebTruyenChu_Backend.Entities;

namespace WebTruyenChu_Backend.DTOs;

public class GetAuthorDto
{
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    
    public List<BookOverviewDto>? Books { get; set; }
}