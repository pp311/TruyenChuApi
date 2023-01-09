using WebTruyenChu_Backend.DTOs;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface IAuthorService
{
    Task<GetAuthorDto> AddAuthor(AddAuthorDto authorDto);
    Task<IEnumerable<GetAuthorDto>> GetAllAuthor();
    Task<GetAuthorDto?> GetAuthorById(int id);
    Task DeleteAuthor(int id);
    Task<GetAuthorDto> UpdateAuthor(UpdateAuthorDto updateAuthorDto);
}