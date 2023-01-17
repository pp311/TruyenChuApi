namespace WebTruyenChu_Backend.DTOs.Responses;

public class PagedResult<T>
{
   public T Data { get; set; }     
   public int CurrentPage { get; set; }
   public int TotalPages { get; set; }
   public int PageSize { get; set; }
   public int TotalCount { get; set; }
   public bool HasPrevious => CurrentPage > 1;
   public bool HasNext => CurrentPage < TotalPages;
    public PagedResult(T items, int count, int pageIndex, int pageSize)
    {
        Data = items;
        TotalCount = count;
        CurrentPage = pageIndex;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}