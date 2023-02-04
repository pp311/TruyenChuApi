using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class UpdateUserDto
{
    [System.Text.Json.Serialization.JsonIgnore] 
    public int UserId {get; set; }
    [Required]
    [MaxLength(256)]
    public string? Name {get; set; }
    [MaxLength(256)]
    public string? Avatar {get; set; }
    [MaxLength(10)]
    public string? Gender {get; set; }
    [MaxLength(1000)]
    public string? Introduction {get; set; }
    public DateTime? DateOfBirth {get; set; }
}
