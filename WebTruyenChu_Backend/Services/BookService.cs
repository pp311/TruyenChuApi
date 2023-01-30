using System.Diagnostics;
using AutoMapper;
using Diacritics.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.DTOs.Responses;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class BookService : IBookService
{
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;

    public BookService(WebTruyenChuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetBookDto> AddBook(AddBookDto addBookDto)
    {
        var book = _mapper.Map<Book>(addBookDto);
        foreach (var genreId in addBookDto.GenreIds)
        {
            book.BookGenres!.Add(new BookGenre() {GenreId = genreId});
        }
        _context.Add(book);
        await _context.SaveChangesAsync();
        return await GetBookById(book.BookId) ?? _mapper.Map<GetBookDto>(book);
    }

    public async Task<GetBookDto?> GetBookById(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .FirstOrDefaultAsync(b => b.BookId == id);
        return _mapper.Map<GetBookDto>(book);
    }
    

    public async Task DeleteBook(int id)
    {
        var book = new Book { BookId = id };
        _context.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<List<BookOverviewDto>>> GetBooks(BookQueryParameters filter)
    {
        var query = _context.Books
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Include(b => b.Author)
            .AsSplitQuery()
            .AsNoTracking();
        if (filter.GenreId is not null)
        {
            query = query.Where(b => b.BookGenres.Any(bg => filter.GenreId.Contains(bg.GenreId)));
        }

        if (filter.Status is not null)
        {
            query = query.Where(b => b.Status == filter.Status);
        }
        query = filter.OrderBy switch
        {
            OrderBy.LatestUpdated => query.OrderBy(b => b.ModifiedAt),
            OrderBy.LatestUploaded => query.OrderBy(b => b.CreatedAt),
            OrderBy.RatingScore => query.Include(b => b.Reviews)
                .OrderBy(b => b.Reviews.Average(rc => rc.Score)),
            OrderBy.RatingCount => query.Include(b => b.Reviews)
                .OrderBy(b => b.Reviews.Count),
            OrderBy.ViewCount => query.OrderBy(b => b.ViewCount),
            OrderBy.ViewCountDay => query.Include(b => b.Chapters).
                ThenInclude(c => c.ReadingHistory)
                .OrderBy(b =>
                b.Chapters.SelectMany(c => c.ReadingHistory.Where(rh => rh.CreatedAt > DateTime.Now.AddDays(-1)))
                    .Count()),
            OrderBy.ViewCountWeek => query.Include(b => b.Chapters)
                .ThenInclude(c => c.ReadingHistory)
                .OrderBy(b =>
                b.Chapters.SelectMany(c => c.ReadingHistory.Where(rh => rh.CreatedAt > DateTime.Now.AddDays(-7)))
                    .Count()),
            OrderBy.ViewCountMonth => query.Include(b => b.Chapters)
                .ThenInclude(c => c.ReadingHistory)
                .OrderBy(b =>
                b.Chapters.SelectMany(c => c.ReadingHistory.Where(rh => rh.CreatedAt > DateTime.Now.AddMonths(-1)))
                    .Count()),
            OrderBy.ViewCountYear => query.Include(b => b.Chapters)
                .ThenInclude(c => c.ReadingHistory)
                .OrderBy(b =>
                b.Chapters.SelectMany(c => c.ReadingHistory.Where(rh => rh.CreatedAt > DateTime.Now.AddYears(-1)))
                    .Count()),
            _ => query
        };
        if (filter.KeyWord is not null)
        {
            string keyWord = filter.KeyWord.RemoveDiacritics();
            query = query.Where(b =>
                EF.Functions.Collate(b.BookName, "Latin1_General_CI_AI").Contains(keyWord) 
                || EF.Functions.Collate(b.Author.AuthorName, "Latin1_General_CI_AI").Contains(keyWord));
        }
        
        var totalCount = await query.CountAsync();
        //default: true
        if (filter.IsDescending)
        {
            query = query.Reverse();
        }
        query = query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize);
        var result = _mapper.Map<List<BookOverviewDto>>(await query.ToListAsync());
        return new PagedResult<List<BookOverviewDto>>(result, totalCount, filter.PageIndex, filter.PageSize);
    }

    public async Task<List<BookOverviewDto>> GetRandomBooks(int limit)
    {
        return _mapper.Map<List<BookOverviewDto>>(await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .OrderBy(b => Guid.NewGuid())
            .Take(limit)
            .ToListAsync());
    }

    public async Task<GetBookDto?> UpdateBook(UpdateBookDto updateBookDto)
    {
        var book = await _context.Books.Include(b => b.BookGenres)
            .FirstOrDefaultAsync(b => b.BookId == updateBookDto.BookId);
        if (book is null)
            return null;
        book = _mapper.Map(updateBookDto, book);
        
        foreach (var bg in book!.BookGenres.ToList())
        {
            if(updateBookDto.GenreIds.Contains(bg.GenreId) is false)
                book.BookGenres!.Remove(bg);
        }

        foreach (var genreId in updateBookDto.GenreIds)
        {
            if(book.BookGenres.Any(bg => bg.GenreId == genreId) is false)
                book.BookGenres.Add(new BookGenre {BookId = book.BookId, GenreId = genreId});
        }
        _context.Update(book);
        await _context.SaveChangesAsync();
        return await GetBookById(book.BookId);
    }

    public async Task<GetBookDto?> PartialUpdateBook(int bookId, JsonPatchDocument<UpdateBookDto> patchDoc)
    {
        var book = await _context.Books.Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .FirstOrDefaultAsync(b => b.BookId == bookId);
        var bookToPatch = _mapper.Map<UpdateBookDto>(book);
        patchDoc.ApplyTo(bookToPatch);
        _mapper.Map(bookToPatch, book);
        if (_context.Entry(book).Property(b => b.AuthorId).IsModified)
        {
            if (await _context.Authors.FindAsync(book.AuthorId) is null) return null;
        }
        foreach (var bg in book!.BookGenres.ToList())
        {
            if(bookToPatch.GenreIds.Contains(bg.GenreId) is false)
                book.BookGenres!.Remove(bg);
        }

        foreach (var genreId in bookToPatch.GenreIds)
        {
            if(book.BookGenres.Any(bg => bg.GenreId == genreId) is false)
                book.BookGenres.Add(new BookGenre {BookId = book.BookId, GenreId = genreId});
        }
        await _context.SaveChangesAsync();
        return await GetBookById(book.BookId);
    }
}