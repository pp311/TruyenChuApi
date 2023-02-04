using WebTruyenChu_Backend.DTOs.Book;

namespace WebTruyenChu_Backend.DTOs.Author;

public class GetAuthorDto
{
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    public List<BookOverviewDto>? Books { get; set; }
    
     public bool ShouldSerializeBooks()
    {
        return Books is not null && Books.Any();
    }
}