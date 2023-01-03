namespace WebTruyenChu_Backend.Entities;

public class Review : AuditableEntity
{
   public int ReviewId { get; set; } 
   public int Score { get; set; }
   public string? Content { get; set; }
   
   public int BookId { get; set; }

   public virtual Book? Book { get; set; }
}