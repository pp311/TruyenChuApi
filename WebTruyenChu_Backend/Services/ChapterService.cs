using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs;
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
    
    public async Task<GetChapterDto?> GetChapterById(int id)
    {
        var chapter = await _context.Chapters.AsNoTracking().FirstOrDefaultAsync(x => x.ChapterId == id);
        return _mapper.Map<GetChapterDto>(chapter);
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

    public async Task<GetChapterDto> AddChapter(AddChapterDto addChapterDto)
    {
        var regex = new Regex("[ ]{2,}", RegexOptions.None);     
        addChapterDto.Content = regex.Replace(addChapterDto.Content.Trim(), " ");
        var wordCount = addChapterDto.Content.Split().Length;
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
        _context.Update(chapter);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetChapterDto>(chapter);
    }
}