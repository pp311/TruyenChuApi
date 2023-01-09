using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService  _authorService ;

    public AuthorController(IAuthorService authorService, IMapper mapper)
    {
       _authorService = authorService;
    }
    
    [HttpPost]
    public async Task<ActionResult<GetAuthorDto>> AddAuthor([FromBody]AddAuthorDto? authorAddDto)
    {
        if (authorAddDto == null) return BadRequest(); 
        var newAuthorDto = await _authorService.AddAuthor(authorAddDto);
        return CreatedAtRoute(nameof(GetAuthorById), new {id = newAuthorDto.AuthorId} ,newAuthorDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAuthorDto>>> GetAllAuthor()
    {
        return Ok(await _authorService.GetAllAuthor());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetAuthorDto>> GetAuthorById(int id)
    {
        var authorDto = await _authorService.GetAuthorById(id);
        if (authorDto is null)
        {
            return NotFound();
        }
        return Ok(authorDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GetAuthorDto>> UpdateAuthor(int id, UpdateAuthorDto? updateAuthorDto)
    {
        if (updateAuthorDto is null)
        {
            return BadRequest();
        }
        if (await _authorService.GetAuthorById(id) is null)
        {
            return NotFound();
        }

        updateAuthorDto.AuthorId = id;
        var updatedAuthorDto = await _authorService.UpdateAuthor(updateAuthorDto);
        return Ok(updatedAuthorDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        if (await _authorService.GetAuthorById(id) is null)
        {
            return NotFound();
        }

        await _authorService.DeleteAuthor(id);
        return NoContent();
    }
}