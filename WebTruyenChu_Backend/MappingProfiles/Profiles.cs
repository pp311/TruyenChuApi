using AutoMapper;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.Author;
using WebTruyenChu_Backend.DTOs.Book;
using WebTruyenChu_Backend.DTOs.Chapter;
using WebTruyenChu_Backend.DTOs.Comment;
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
        CreateMap<Chapter, GetChapterDetailDto>()
          .ForMember(dto => dto.BookName, opt => opt.MapFrom(c => c.Book.BookName))
          .ForMember(dto => dto.AuthorName, opt => opt.MapFrom(c => c.Book.Author.AuthorName))
          .ForMember(dto => dto.AuthorId, opt => opt.MapFrom(c => c.Book.AuthorId));
        CreateMap<AddChapterDto, Chapter>();
        CreateMap<UpdateChapterDto, Chapter>().ReverseMap();
        CreateMap<GetChapterDto, UpdateChapterDto>().ReverseMap();

        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForMember(u => u.Id, opt => opt.MapFrom(dto => dto.UserId))
            .ReverseMap()
            .ForMember(dto => dto.UserId, opt => opt.MapFrom(u => u.Id));
        CreateMap<User, GetUserDto>().ForMember(dto => dto.UserId, opt => opt.MapFrom(u => u.Id));

        CreateMap<AddBookCommentDto, BookComment>();
        CreateMap<AddChapterCommentDto, ChapterComment>();
        CreateMap<BookComment, GetCommentDto>()
            .ForMember(dto => dto.Time, opt => opt.MapFrom(bc => bc.ModifiedAt))
            .ForMember(dto => dto.UserName, opt => opt.MapFrom(c => c.User.UserName))
            .ForMember(dto => dto.Avatar, opt => opt.MapFrom(c => c.User.Avatar));
        CreateMap<BookComment, GetBookCommentDto>();
        CreateMap<ChapterComment, GetCommentDto>()
            .ForMember(dto => dto.Time, opt => opt.MapFrom(bc => bc.ModifiedAt))
            .ForMember(dto => dto.UserName, opt => opt.MapFrom(c => c.User.UserName))
            .ForMember(dto => dto.Avatar, opt => opt.MapFrom(c => c.User.Avatar));
        CreateMap<ChapterComment, GetChapterCommentDto>();
        CreateMap<UpdateCommentDto, Comment>();
    }
}
