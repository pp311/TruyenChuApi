using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs.Comment;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.DTOs.Responses;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class CommentService : ICommentService
{
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;

    public CommentService(WebTruyenChuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> IsCommentExist(int commentId)
    {
        if (await _context.Comments.FindAsync(commentId) is not null)
        {
            return true;
        }
        return false;
    }

    public async Task<GetCommentDto?> GetCommentById(int commentId)
    {
        var comment = await _context.Comments
            .AsNoTracking()
            .Include(bc => bc.User)
            .FirstOrDefaultAsync(b => b.CommentId == commentId);
        return _mapper.Map<GetCommentDto>(comment);
    }

    public async Task<GetBookCommentDto?> GetBookCommentById(int commentId)
    {
        var bookComment = await _context.BookComments
            .AsNoTracking()
            .Include(bc => bc.User)
            .FirstOrDefaultAsync(b => b.CommentId == commentId);
        return _mapper.Map<GetBookCommentDto>(bookComment);
    }

    public async Task<GetChapterCommentDto?> GetChapterCommentById(int commentId)
    {
        var chapterComment = await _context.ChapterComments
            .AsNoTracking()
            .Include(bc => bc.User)
            .FirstOrDefaultAsync(b => b.CommentId == commentId);
        return _mapper.Map<GetChapterCommentDto>(chapterComment);
    }

    public async Task<PagedResult<List<GetBookCommentDto>>> GetBookCommentsWithPagination(int bookId, CommentQueryParameters filter)
    {
        var commentList = _context.BookComments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.BookId == bookId);
        commentList = filter.OrderBy switch
        {
            OrderBy.Newest => commentList.OrderBy(c => c.ModifiedAt),
            OrderBy.Oldest => commentList.OrderByDescending(c => c.ModifiedAt),
            OrderBy.LikeCount => commentList.Include(c => c.CommentsReactions)
               .OrderBy(c => c.CommentsReactions.Count),
            _ => commentList.OrderBy(c => c.ModifiedAt)
        };
        
        var totalCount = await _context.BookComments.CountAsync(x => x.BookId == bookId);
        commentList = commentList.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize);
        var result = _mapper.Map<List<GetBookCommentDto>>(await commentList.ToListAsync());
        return new PagedResult<List<GetBookCommentDto>>(result, totalCount, filter.PageIndex, filter.PageSize);
    }

    public async Task<PagedResult<List<GetChapterCommentDto>>> GetChapterCommentsWithPagination(int chapterId, CommentQueryParameters filter)
    {
        var commentList = _context.ChapterComments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.ChapterId == chapterId);
        commentList = filter.OrderBy switch
        {
            OrderBy.Newest => commentList.OrderBy(c => c.ModifiedAt),
            OrderBy.Oldest => commentList.OrderByDescending(c => c.ModifiedAt),
            OrderBy.LikeCount => commentList.Include(c => c.CommentsReactions)
               .OrderBy(c => c.CommentsReactions.Count),
            _ => commentList.OrderBy(c => c.ModifiedAt)
        };
        
        var totalCount = await _context.ChapterComments.CountAsync(x => x.ChapterId == chapterId);
        commentList = commentList.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize);
        var result = _mapper.Map<List<GetChapterCommentDto>>(await commentList.ToListAsync());
        return new PagedResult<List<GetChapterCommentDto>>(result, totalCount, filter.PageIndex, filter.PageSize);
    }

    public async Task<GetBookCommentDto> AddBookComment(AddBookCommentDto dto)
    {
        var bookComment = _mapper.Map<BookComment>(dto);
        _context.Add(bookComment);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetBookCommentDto>(bookComment);
    }

    public async Task<GetChapterCommentDto> AddChapterComment(AddChapterCommentDto dto)
    {
        var chapterComment = _mapper.Map<ChapterComment>(dto);
        _context.Add(chapterComment);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetChapterCommentDto>(chapterComment);
    }

    public async Task<GetCommentDto> UpdateComment(UpdateCommentDto dto)
    {
        var comment = await _context.Comments.FindAsync(dto.CommentId);
        comment.Content = dto.Content;
        _context.Update(comment);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetCommentDto>(comment);
    }

    public async Task DeleteComment(int commentId)
    {
        var bookComment = new Comment { CommentId = commentId };
        _context.Comments.Remove(bookComment);
        await _context.SaveChangesAsync();
    }
    
    
}