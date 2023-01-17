namespace WebTruyenChu_Backend.DTOs.QueryParameters;

public class PagingParameters
{
    private const int MaxPageSize = 50;
    private const int DefaultPageSize = 10;
    public int PageIndex { get; set; } = 1;
    private int _pageSize { get; set; } = DefaultPageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Enumerable.Range(1, MaxPageSize).Contains(value) ? value : DefaultPageSize;
    }
}