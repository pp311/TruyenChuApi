using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class AddAuthorDto
{
    [MaxLength(256, ErrorMessage = "Author name can't be longer than 256 characters")]
    [Required(ErrorMessage = "AuthorName is required")]
    public string? AuthorName { get; set; }
}