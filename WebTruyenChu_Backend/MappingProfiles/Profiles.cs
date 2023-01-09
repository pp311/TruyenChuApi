using AutoMapper;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Entities;

namespace WebTruyenChu_Backend.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<AddAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
        CreateMap<Author, GetAuthorDto>();
        CreateMap<Book, BookOverviewDto>();
        CreateMap<Book, BookDetailDto>();
    }
}