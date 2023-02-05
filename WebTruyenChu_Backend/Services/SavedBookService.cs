using AutoMapper;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs.Book;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class SavedBookService : ISavedBookService
{
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;

    public SavedBookService(WebTruyenChuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task FollowBook(int bookId, int userId)
    {
        _context.SavedBooks.Add(new SavedBook { BookId = bookId, UserId = userId});
        await _context.SaveChangesAsync();
    }

    public async Task UnfollowBook(int bookId, int userId)
    {
        _context.SavedBooks.Remove(new SavedBook { BookId = bookId, UserId = userId});
        await _context.SaveChangesAsync(); 
    }
}