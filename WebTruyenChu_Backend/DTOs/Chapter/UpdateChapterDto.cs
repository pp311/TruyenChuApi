using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs.Chapter;

public class UpdateChapterDto
{
    [System.Text.Json.Serialization.JsonIgnore] 
    public int ChapterId { get; set; } 
    [Required]
    [MaxLength(256)]
    public string? ChapterName { get; set; }

    [Required] 
    public string Content { get; set; } = null!;
    
    public int BookId { get; set; }
}