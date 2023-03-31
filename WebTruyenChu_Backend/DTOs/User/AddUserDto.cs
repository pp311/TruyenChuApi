using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class AddUserDto
{
    // [Required]
    [MaxLength(256)]
    public string? Name { get; set; } = "";
    [Required]
    [MaxLength(256)]
    public string? UserName { get; set; }

    [Required]
    [MaxLength(256)]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }

    [MaxLength(10)]
    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }
}
