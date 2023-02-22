using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.DTOs.Comment;
using WebTruyenChu_Backend.DTOs.QueryParameters;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
   private readonly ICommentService _commentService;
   private readonly IBookService _bookService;
   private readonly IChapterService _chapterService;

   public CommentController(ICommentService commentService, IBookService bookService, IChapterService chapterService)
   {
      _commentService = commentService;
      _bookService = bookService;
      _chapterService = chapterService;
   }

   /*[HttpGet("book/{commentId:int}", Name = "GetBookCommentById")]
   public async Task<ActionResult<GetBookCommentDto>> GetBookCommentById(int commentId)
   {
      var dto = await _commentService.GetBookCommentById(commentId);
      if(dto is null) return NotFound();
      return Ok(dto);
   }
   
   [HttpGet("chapter/{chapterId:int}", Name = "GetChapterCommentById")]
   public async Task<ActionResult<GetChapterCommentDto>> GetChapterCommentById(int chapterId)
   {
      var dto = await _commentService.GetChapterCommentById(chapterId);
      if(dto is null) return NotFound();
      return Ok(dto);
   }*/

   [HttpGet("{id:int}", Name = "GetCommentById")]
   public async Task<IActionResult> GetCommentById(int id)
   {
      var dto = await _commentService.GetBookCommentById(id);
      if (dto is null)
      {
         var chapterCommentDto = await _commentService.GetChapterCommentById(id);
         if (chapterCommentDto is null) return NotFound();
         return Ok(chapterCommentDto);
      }

      return Ok(dto);
   }

   [HttpPost("book")]
   [Authorize]
   public async Task<IActionResult> AddBookComment([FromBody] AddBookCommentDto? dto)
   {
      if (dto is null) return BadRequest();
      if (await _bookService.GetBookById(dto.BookId) is null) return NotFound("Book ID not found");
      dto.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
      var newBookCommentDto = await _commentService.AddBookComment(dto);
      return CreatedAtRoute(nameof(GetCommentById), new { id = newBookCommentDto.CommentId }, newBookCommentDto);
   }

   [HttpPost("chapter")]
   [Authorize]
   public async Task<IActionResult> AddChapterComment([FromBody] AddChapterCommentDto? dto)
   {
      if (dto is null) return BadRequest();
      if (await _chapterService.GetChapterById(dto.ChapterId) is null) return NotFound("Chapter ID not found");
      dto.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
      var newChapterCommentDto = await _commentService.AddChapterComment(dto);
      return CreatedAtRoute(nameof(GetCommentById), new { id = newChapterCommentDto.CommentId }, newChapterCommentDto);
   }

   [HttpDelete("{id:int}")]
   [Authorize]
   public async Task<IActionResult> DeleteComment(int id)
   {
      var commentDto = await _commentService.GetCommentById(id);
      if (commentDto is not null)
      {
         if (commentDto.UserId != Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) &&
             User.FindFirstValue(ClaimTypes.Role) != Role.Admin)
         {
            return Unauthorized();
         }
         await _commentService.DeleteComment(id);
         return NoContent();
      }

      return NotFound();
   }

   [HttpGet("book/{bookId:int}")]
   public async Task<ActionResult<IEnumerable<GetBookCommentDto>>> GetCommentsByBookId(
      [FromQuery] CommentQueryParameters filter, int bookId)
   {
      var commentList = await _commentService.GetBookCommentsWithPagination(bookId, filter);
      var metadata = new
      {
         commentList.TotalCount,
         commentList.PageSize,
         commentList.CurrentPage,
         commentList.TotalPages,
         commentList.HasNext,
         commentList.HasPrevious
      };
      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
      return Ok(commentList.Data);
   }

   [HttpGet("chapter/{chapterId:int}")]
   public async Task<ActionResult<IEnumerable<GetChapterCommentDto>>> GetCommentsChapterId(
      [FromQuery] CommentQueryParameters filter, int chapterId)
   {
      var commentList = await _commentService.GetChapterCommentsWithPagination(chapterId, filter);
      var metadata = new
      {
         commentList.TotalCount,
         commentList.PageSize,
         commentList.CurrentPage,
         commentList.TotalPages,
         commentList.HasNext,
         commentList.HasPrevious
      };
      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
      return Ok(commentList.Data);
   }

   [HttpPut("{id:int}")]
   [Authorize]
   public async Task<ActionResult<GetCommentDto>> UpdateComment(int id, [FromBody] UpdateCommentDto? dto)
   {
      if (!await _commentService.IsCommentExist(id))
      {
         return NotFound();
      }

      if (dto is null)
      {
         return BadRequest();
      }
      var commentDto = await _commentService.GetCommentById(id);
      if (commentDto.UserId != Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) &&
          User.FindFirstValue(ClaimTypes.Role) != Role.Admin)
      {
         return Unauthorized();
      }
      var updatedCommentDto = await _commentService.UpdateComment(dto);
      return Ok(updatedCommentDto);
   }
}