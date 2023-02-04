using Microsoft.AspNetCore.Mvc;
using WebTruyenChu_Backend.DTOs.Book;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    
   private readonly IGenreService _genreService;
    
   public GenreController(IGenreService genreService)
   {
       _genreService = genreService;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
   {
       return Ok(await _genreService.GetAllGenre());
   }
}