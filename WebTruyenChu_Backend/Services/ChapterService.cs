using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs.Chapter;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.DTOs.Responses;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class ChapterService : IChapterService
{
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;

    public ChapterService(WebTruyenChuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetChapterDetailDto?> GetChapterById(int id)
    {
        var chapter = await _context.Chapters.AsNoTracking().Include(c => c.Book).FirstOrDefaultAsync(x => x.ChapterId == id);
        var chapterDto = _mapper.Map<GetChapterDetailDto>(chapter);
        var chaptersList = await _context.Chapters.AsNoTracking().Where(x => x.BookId == chapter.BookId).OrderBy(x => x.ChapterIndex).ToListAsync();

        chapterDto.BookName = chapter.Book.BookName;
        chapterDto.PreviousChapterId = chapterDto.ChapterIndex == 1 ? 0 : chaptersList.Find(x => x.ChapterIndex == chapterDto.ChapterIndex - 1)!.ChapterId;
        chapterDto.NextChapterId = chapterDto.ChapterIndex == chaptersList.Count ? 0 : chaptersList.Find(x => x.ChapterIndex == chapterDto.ChapterIndex + 1)!.ChapterId;
        return chapterDto;
    }

    public async Task<PagedResult<List<GetChapterDto>>> GetChaptersWithPagination(PagingParameters filter, int bookId)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
            cfg.CreateProjection<Chapter, GetChapterDto>()
                .ForMember(x => x.Content, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.ModifiedAt, opt => opt.Ignore())
        );
        var chapterList = await _context.Chapters.AsNoTracking()
            .Where(x => x.BookId == bookId)
            .Skip((filter.PageIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ProjectTo<GetChapterDto>(mapperConfig)
            .ToListAsync();
        var totalCount = await _context.Chapters.CountAsync(x => x.BookId == bookId);
        return new PagedResult<List<GetChapterDto>>(chapterList, totalCount, filter.PageIndex, filter.PageSize);
    }

    public async Task<List<GetChapterDetailDto>> GetLatestChapters(int limit)
    {
        var chapterList = await _context.Chapters.AsNoTracking()
            .Include(c => c.Book)
            .ThenInclude(b => b.Author)
            .GroupBy(c => c.BookId)
            .Select(c => c.OrderByDescending(c => c.CreatedAt).First())
            .Take(limit)
            // .OrderByDescending(g => g.CreatedAt)
            .ToListAsync();
        return _mapper.Map<List<GetChapterDetailDto>>(chapterList);
    }


    public async Task<GetChapterDto> AddChapter(AddChapterDto addChapterDto)
    {
        (var wordCount, addChapterDto.Content) = TrimAndCountWord(addChapterDto.Content);
        var chapter = _mapper.Map<Chapter>(addChapterDto);
        chapter.WordCount = wordCount;
        _context.Add(chapter);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetChapterDto>(chapter);
    }

    public async Task DeleteChapter(int id)
    {
        var chapter = new Chapter { ChapterId = id };
        _context.Remove(chapter);
        await _context.SaveChangesAsync();
    }

    public async Task<GetChapterDto?> UpdateChapter(UpdateChapterDto updateChapterDto)
    {
        var chapter = _mapper.Map<Chapter>(updateChapterDto);
        (chapter.WordCount, chapter.Content) = TrimAndCountWord(updateChapterDto.Content);
        _context.Update(chapter);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetChapterDto>(chapter);
    }

    public async Task<GetChapterDto?> PartialUpdateChapter(int chapterId, JsonPatchDocument<UpdateChapterDto> patchDoc)
    {
        var chapter = await _context.Chapters.FindAsync(chapterId);
        var chapterToPatch = _mapper.Map<UpdateChapterDto>(chapter);
        patchDoc.ApplyTo(chapterToPatch);
        _mapper.Map(chapterToPatch, chapter);

        if (_context.Entry(chapter).Property(c => c.Content).IsModified)
        {
            (chapter.WordCount, chapter.Content) = TrimAndCountWord(chapterToPatch.Content);
        }

        if (_context.Entry(chapter).Property(c => c.BookId).IsModified)
        {
            if (await _context.Books.FindAsync(chapter.BookId) is null) return null;
        }
        await _context.SaveChangesAsync();
        return _mapper.Map<GetChapterDto>(chapter);
    }

    private (int wc, string res) TrimAndCountWord(string content)
    {
        var regex = new Regex("[ ]{2,}", RegexOptions.None);
        content = regex.Replace(content.Trim(), " ");
        return (content.Split().Length, content);
    }
}
