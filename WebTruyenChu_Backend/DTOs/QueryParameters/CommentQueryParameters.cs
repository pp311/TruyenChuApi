namespace WebTruyenChu_Backend.DTOs.QueryParameters;

public class CommentQueryParameters : PagingParameters
{
   public string OrderBy { get; set; } = Constants.OrderBy.Newest;
}