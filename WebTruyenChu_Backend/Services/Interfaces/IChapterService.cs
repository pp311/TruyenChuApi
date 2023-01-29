using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.DTOs.Responses;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface IChapterService
{
   Task<GetChapterDto?> GetChapterById(int id);
   Task<PagedResult<List<GetChapterDto>>> GetChaptersWithPagination(PagingParameters parameters, int bookId);
   Task<GetChapterDto> AddChapter(AddChapterDto addChapterDto);
   Task DeleteChapter(int id);
   Task<GetChapterDto?> UpdateChapter(UpdateChapterDto updateChapterDto);
   
}