using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class GenreService : IGenreService
{
    
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;

    public GenreService(WebTruyenChuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GenreDto>> GetAllGenre()
    {
        return _mapper.Map<List<GenreDto>>(await _context.Genres.ToListAsync());
    }

   // public async Task<GenreDto> AddGenre(GenreDto genreDto)
   // {
   //     var genre = _mapper.Map<Genre>(genreDto);
   //     _context.Add(genre);
   //     await _context.SaveChangesAsync();
   //     return _mapper.Map<GenreDto>(genre);
   // }

   // public async Task<GenreDto> UpdateGenre(GenreDto genreDto)
   // {
   //     var genre = _mapper.Map<Genre>(genreDto);
   //     _context.Update(genre);
   //     await _context.SaveChangesAsync();
   //     return _mapper.Map<GenreDto>(genre);
   //     
   // }

   // public async Task DeleteGenre(int id)
   // {
   //     var genre = new Genre { GenreId = id };
   //     _context.Remove(genre);
   //     await _context.SaveChangesAsync();
   // }
}