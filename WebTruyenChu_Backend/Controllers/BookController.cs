using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.DTOs.Book;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
   private readonly IBookService _bookService;
   private readonly ISavedBookService _savedBookService;
   private readonly IMapper _mapper;
    
   public BookController(IBookService bookService, IMapper mapper, ISavedBookService savedBookService)
   {
       _bookService = bookService;
       _mapper = mapper;
       _savedBookService = savedBookService;
   }

   [HttpPost]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult<GetBookDto>> AddBook([FromBody]AddBookDto? addBookDto)
   {
        if (addBookDto is null) return BadRequest(); 
        var newBookDto = await _bookService.AddBook(addBookDto);
        return CreatedAtRoute(nameof(GetBookById), new {id = newBookDto.BookId} ,newBookDto);
   }

   [HttpGet("{id:int}", Name = "GetBookById")]
   public async Task<ActionResult<GetBookDto>> GetBookById(int id)
   {
       var bookDto = await _bookService.GetBookById(id);
       if (bookDto is null) return NotFound();
       return Ok(bookDto);
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<BookOverviewDto>>> GetBooks([FromQuery]BookQueryParameters filter)
   {
       var bookList = await _bookService.GetBooks(filter);
       var metadata = new
        {
            bookList.TotalCount,
            bookList.PageSize,
            bookList.CurrentPage,
            bookList.TotalPages,
            bookList.HasNext,
            bookList.HasPrevious
        };
       Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
       return Ok(bookList.Data);
   }

   [HttpGet("random")]
   public async Task<ActionResult<IEnumerable<BookOverviewDto>>> GetRandomBooks(int limit = 10)
   {
       return Ok(await _bookService.GetRandomBooks(limit));
   }

   [HttpDelete("{id:int}")]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult> DeleteBook(int id)
   {
       if (await _bookService.GetBookById(id) is null)
       {
           return NotFound();
       }
       await _bookService.DeleteBook(id);
       return NoContent();
   }

   [HttpPut("{id:int}")]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult<GetBookDto>> UpdateBook(int id, [FromBody] UpdateBookDto? updateBookDto)
   {
        if (updateBookDto is null)
        {
            return BadRequest();
        }
        updateBookDto.BookId = id;
        var updatedBookDto = await _bookService.UpdateBook(updateBookDto);
        if (updatedBookDto is null)
            return NotFound();
        return Ok(updatedBookDto);
   }

   [HttpPatch("{id:int}")]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult<GetBookDto>> PartialUpdateBook(int id,
       [FromBody] JsonPatchDocument<UpdateBookDto> patchDoc)
   {
        var book = await _bookService.GetBookById(id);
        if(book is null) return NotFound("Book not found");
        var patchDto = _mapper.Map<UpdateBookDto>(book);
        patchDoc.ApplyTo(patchDto);
        if (!TryValidateModel(patchDto))
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors });
            return BadRequest(errors);
        }
        var updatedBookDto = await _bookService.PartialUpdateBook(id, patchDoc); 
        if(updatedBookDto is null) return NotFound("Author not found");
        return Ok(updatedBookDto);
   }

   [HttpPost("follow")]
   [Authorize]
   public async Task<ActionResult> FollowBook(int bookId)
   {
       try
       {
           await _savedBookService.FollowBook(bookId, Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
       }
       catch (DbUpdateConcurrencyException)
       {
           return NotFound("Book not found");
       }
       return Ok();
   }
   
   [HttpPost("unfollow")]
   [Authorize]
   public async Task<ActionResult> UnfollowBook(int bookId)
   {
       try
       {
            await _savedBookService.UnfollowBook(bookId, Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
       }
       catch (DbUpdateConcurrencyException)
       {
           return NotFound("Book not found");
       }
       return Ok();
   }
}