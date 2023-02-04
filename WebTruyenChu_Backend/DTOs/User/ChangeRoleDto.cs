using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class ChangeRoleDto
{
    [Required]
    public string? Role { get; set; }
}