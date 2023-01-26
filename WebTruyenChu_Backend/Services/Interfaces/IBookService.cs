using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.DTOs.Responses;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface IBookService
{

    Task<GetBookDto> AddBook(AddBookDto addBookDto);
    Task<GetBookDto?> GetBookById(int id);
    Task DeleteBook(int id);
    Task<PagedResult<List<BookOverviewDto>>> GetBooks(BookQueryParameters filter);
    
    Task<List<BookOverviewDto>> GetRandomBooks(int limit);
    Task<GetBookDto?> UpdateBook(UpdateBookDto updateBookDto);
}