using WebTruyenChu_Backend.DTOs.Book;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface ISavedBookService
{
    Task FollowBook(int bookId, int userId);
    Task UnfollowBook(int bookId, int userId);
}