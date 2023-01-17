using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class AddAuthorDto
{
    [MaxLength(256)]
    [Required]
    public string? AuthorName { get; set; }
}