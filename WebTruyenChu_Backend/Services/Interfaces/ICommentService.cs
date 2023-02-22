using WebTruyenChu_Backend.DTOs.Comment;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.DTOs.Responses;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface ICommentService
{
    Task<bool> IsCommentExist(int commentId);
    Task<GetCommentDto?> GetCommentById(int commentId);
    Task<GetBookCommentDto?> GetBookCommentById(int commentId);
    Task<GetChapterCommentDto?> GetChapterCommentById(int commentId);
    Task<PagedResult<List<GetBookCommentDto>>> GetBookCommentsWithPagination(int bookId, CommentQueryParameters filter);
    Task<PagedResult<List<GetChapterCommentDto>>> GetChapterCommentsWithPagination(int chapterId, CommentQueryParameters filter);
    Task<GetBookCommentDto> AddBookComment(AddBookCommentDto dto);
    Task<GetChapterCommentDto> AddChapterComment(AddChapterCommentDto dto);
    Task<GetCommentDto> UpdateComment(UpdateCommentDto dto);
    Task DeleteComment(int commentId);
}