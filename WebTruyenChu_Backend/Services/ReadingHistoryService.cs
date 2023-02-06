using AutoMapper;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class ReadingHistoryService : IReadingHistoryService
{
    private readonly WebTruyenChuContext _context;

    public ReadingHistoryService(WebTruyenChuContext context)
    {
        _context = context;
    }
    public async Task AddReadingHistory(int chapterId, int userId)
    {
        _context.ReadingHistories.Add(new ReadingHistory { ChapterId = chapterId, UserId = userId });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveReadingHistory(int chapterId, int userId)
    {
        _context.ReadingHistories.Remove(new ReadingHistory { ChapterId = chapterId, UserId = userId });
        await _context.SaveChangesAsync();
    }
}