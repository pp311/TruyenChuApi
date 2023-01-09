using AutoMapper;
using WebTruyenChu_Backend.Data;
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
}