using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class AddChapterDto
{
    [Required]
    [MaxLength(256)]
    public string? ChapterName { get; set; }

    [Required] public string Content { get; set; } = null!;
    
    [System.Text.Json.Serialization.JsonIgnore] 
    public int BookId { get; set; }}
    