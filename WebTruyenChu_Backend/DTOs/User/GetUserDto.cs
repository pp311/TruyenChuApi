namespace WebTruyenChu_Backend.DTOs;

public class GetUserDto
{
   public int UserId { get; set; }
   public string? UserName { get; set; }
   public string? Email { get; set; }
   public string? Name { get; set; }
   public string? Gender { get; set; }
   public string? Introduction { get; set; }
   public DateTime DateOfBirth { get; set; }
}