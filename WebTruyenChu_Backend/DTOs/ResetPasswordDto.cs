using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class ResetPasswordDto
{
    [Required]
    public string? Password { get; set; }
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Token { get; set; }
}