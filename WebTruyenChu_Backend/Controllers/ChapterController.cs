using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.DTOs.Chapter;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChapterController : ControllerBase
{
   private readonly IChapterService _chapterService;
   private readonly IBookService _bookService;
   private readonly IMapper _mapper;

   public ChapterController(IChapterService chapterService, IBookService bookService, IMapper mapper)
   {
       _chapterService = chapterService;
       _bookService = bookService;
       _mapper = mapper;
   }

   [HttpGet("{id:int}", Name = "GetChapterById")]
   public async Task<ActionResult<GetChapterDto>> GetChapterById(int id)
   {
       var chapterDto = await _chapterService.GetChapterById(id);
       if(chapterDto is null) return NotFound();
       return Ok(chapterDto);
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<GetChapterDto>>> GetChapters([FromQuery]PagingParameters filter,[FromQuery] int bookId)
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
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult<GetChapterDto>> AddChapter([FromBody]AddChapterDto? addChapterDto)
   {
       if (addChapterDto is null) return BadRequest();
       if (await _bookService.GetBookById(addChapterDto.BookId) is null) return NotFound("Book ID not found");
       var newChapterDto = await _chapterService.AddChapter(addChapterDto);
       return CreatedAtRoute(nameof(GetChapterById), new { id = newChapterDto.BookId }, newChapterDto);
   }
   
   [HttpDelete("{id:int}")]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult> DeleteBookById(int id)
   {
        if(await _chapterService.GetChapterById(id) is null) return NotFound();
        await _chapterService.DeleteChapter(id);
        return NoContent();
   }

   [HttpPut("{id:int}")]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult<GetChapterDto>> UpdateChapter([FromBody] UpdateChapterDto? updateChapterDto, int id)
   {
       if (updateChapterDto is null) return BadRequest();
       if (await _bookService.GetBookById(updateChapterDto.BookId) is null) return NotFound("Book ID not found");
       updateChapterDto.ChapterId = id;
       var updatedChapterDto = await _chapterService.UpdateChapter(updateChapterDto);
       if(updatedChapterDto is null) return NotFound("Chapter ID not found");
       return Ok(updatedChapterDto);
   }

   [HttpPatch("{id:int}")]
   [Authorize(Roles = Role.AdminAndEditor)]
   public async Task<ActionResult<GetChapterDto>> PartialUpdateChapter(
       [FromBody] JsonPatchDocument<UpdateChapterDto> patchDoc, int id)
   {
        var original = await _chapterService.GetChapterById(id);
        if (original is null) return NotFound("Chapter not found");
        var patchDto = _mapper.Map<UpdateChapterDto>(original);
        patchDoc.ApplyTo(patchDto);
        if (!TryValidateModel(patchDto))
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors });
            return BadRequest(errors);
        }

        var updatedChapterDto = await _chapterService.PartialUpdateChapter(id, patchDoc); 
        if(updatedChapterDto is null) return NotFound("Book id not found");
        return Ok(updatedChapterDto);
   }
}