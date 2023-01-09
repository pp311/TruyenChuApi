using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class AuthorService : IAuthorService
{
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;

    public AuthorService(WebTruyenChuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAuthorDto> AddAuthor(AddAuthorDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        _context.Add(author);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetAuthorDto>(author);
    }

    public async Task<IEnumerable<GetAuthorDto>> GetAllAuthor()
    {
        var authorList = await _context.Authors.ToListAsync();
        return _mapper.Map<List<GetAuthorDto>>(authorList);
    }

    public async Task<GetAuthorDto?> GetAuthorById(int id)
    {
        var author = await _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.AuthorId == id);
        return _mapper.Map<GetAuthorDto>(author);
    }

    public async Task DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        _context.Remove(author!);
        await _context.SaveChangesAsync();
    }

    public async Task<GetAuthorDto> UpdateAuthor(UpdateAuthorDto updateAuthorDto)
    {
        var author = _mapper.Map<Author>(updateAuthorDto);
        _context.Update(author);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetAuthorDto>(author);
    }
}