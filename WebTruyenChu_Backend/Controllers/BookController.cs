using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.Services;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
   private readonly IBookService _bookService;
    
   public BookController(IBookService bookService)
   {
       _bookService = bookService;
   }

   [HttpPost]
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
}