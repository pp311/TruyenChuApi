using AutoMapper;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Entities;

namespace WebTruyenChu_Backend.MappingProfiles;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<AddAuthorDto, Author>();
        CreateMap<UpdateAuthorDto, Author>();
        CreateMap<Author, GetAuthorDto>();

        CreateMap<AddBookDto, Book>();
        CreateMap<UpdateBookDto, Book>()
            .ReverseMap()
            .ForMember(dto => dto.GenreIds, opt => opt.MapFrom(b => b.BookGenres.Select(bg => bg.GenreId).ToList()));
        CreateMap<UpdateBookDto, GetBookDto>().ReverseMap();
        CreateMap<Book, BookOverviewDto>()
            .ForMember(bo => bo.Genres, opt => opt.MapFrom(b => b.BookGenres.Select(bg => bg.Genre).ToList()))
            .ForMember(bd => bd.AuthorId, opt => opt.MapFrom(b => b.AuthorId))
            .ForMember(bo => bo.AuthorName, opt => opt.MapFrom(b => b.Author.AuthorName));
        CreateMap<Book, GetBookDto>()
            .ForMember(bd => bd.Genres, opt => opt.MapFrom(b => b.BookGenres.Select(bg => bg.Genre).ToList()))
            .ForMember(bd => bd.AuthorId, opt => opt.MapFrom(b => b.AuthorId))
            .ForMember(bd => bd.AuthorName, opt => opt.MapFrom(b => b.Author.AuthorName));
        CreateMap<GetBookDto, UpdateBookDto>().ForMember(bd => bd.GenreIds,
            opt => opt.MapFrom(bd => bd.Genres.Select(g => g.GenreId).ToList()));

        CreateMap<Chapter, GetChapterDto>();
        CreateMap<AddChapterDto, Chapter>();
        CreateMap<UpdateChapterDto, Chapter>().ReverseMap();
        CreateMap<GetChapterDto, UpdateChapterDto>().ReverseMap();

        CreateMap<AddUserDto, User>();
    }
}