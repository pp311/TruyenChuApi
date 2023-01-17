namespace WebTruyenChu_Backend.DTOs.QueryParameters;

public class BookQueryParameters : PagingParameters
{
   public string OrderBy { get; set; } = Constants.OrderBy.LatestUpdated;
   public bool IsDescending { get; set; } = true;
   public string? Status { get; set; }
   public List<int>? GenreId { get; set; }
   public string? KeyWord { get; set; }
}