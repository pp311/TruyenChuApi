using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_Backend.DTOs;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    //[Required]
    //public string? ClientUri { get; set; }
}