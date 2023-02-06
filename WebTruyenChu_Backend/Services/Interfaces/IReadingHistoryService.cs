namespace WebTruyenChu_Backend.Services.Interfaces;

public interface IReadingHistoryService
{
    Task AddReadingHistory(int chapterId, int userId);
    Task RemoveReadingHistory(int chapterId, int userId);
}