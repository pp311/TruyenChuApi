using WebTruyenChu_Backend.DTOs.Book;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenre();
    //Task<GenreDto> AddGenre(GenreDto genreDto);
    //Task<GenreDto> UpdateGenre(GenreDto genreDto);
    //Task DeleteGenre(int id);
}