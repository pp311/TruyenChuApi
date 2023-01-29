using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;
[ApiController]
[Route("api/Book/{bookId:int}/[controller]")]
public class ChapterController : ControllerBase
{
   private readonly IChapterService _chapterService;
   private readonly IBookService _bookService;

   public ChapterController(IChapterService chapterService, IBookService bookService)
   {
       _chapterService = chapterService;
       _bookService = bookService;
   }

   [HttpGet("/api/[controller]/{id:int}", Name = "GetChapterById")]
   public async Task<ActionResult<GetChapterDto>> GetChapterById(int id)
   {
       var chapterDto = await _chapterService.GetChapterById(id);
       if(chapterDto is null) return NotFound();
       return Ok(chapterDto);
   }

   [HttpGet("list")]
   public async Task<ActionResult<IEnumerable<GetChapterDto>>> GetChapters([FromQuery]PagingParameters filter, int bookId)
   {
      if(await _bookService.GetBookById(bookId) is null) return NotFound();
      var chaptersDto = await _chapterService.GetChaptersWithPagination(filter, bookId);
      var metadata = new
        {
            chaptersDto.TotalCount,
            chaptersDto.PageSize,
            chaptersDto.CurrentPage,
            chaptersDto.TotalPages,
            chaptersDto.HasNext,
            chaptersDto.HasPrevious
        };
       Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
       return Ok(chaptersDto.Data);
   }

   [HttpPost]
   public async Task<ActionResult<GetChapterDto>> AddChapter([FromBody]AddChapterDto? addChapterDto, int bookId)
   {
       if (addChapterDto is null) return BadRequest();
       if (await _bookService.GetBookById(bookId) is null) return NotFound("Book ID not found");
       addChapterDto.BookId = bookId;
       var newChapterDto = await _chapterService.AddChapter(addChapterDto);
       return CreatedAtRoute(nameof(GetChapterById), new { id = newChapterDto.BookId }, newChapterDto);
   }
   
   [HttpDelete("/api/[controller]/{id:int}")]
   public async Task<ActionResult> DeleteBookById(int id)
   {
        if(await _chapterService.GetChapterById(id) is null) return NotFound();
        await _chapterService.DeleteChapter(id);
        return NoContent();
   }

   [HttpPut("{id:int}")]
   public async Task<ActionResult<GetChapterDto>> UpdateChapter([FromBody] UpdateChapterDto? updateChapterDto, int id, int bookId)
   {
       if (updateChapterDto is null) return BadRequest();
       if (await _bookService.GetBookById(bookId) is null) return NotFound("Book ID not found");
       updateChapterDto.ChapterId = id;
       updateChapterDto.BookId = bookId;
       var updatedChapterDto = await _chapterService.UpdateChapter(updateChapterDto);
       if(updatedChapterDto is null) return NotFound("Chapter ID not found");
       return Ok(updatedChapterDto);
   }
}