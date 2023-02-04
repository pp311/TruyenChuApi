using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.DTOs.Chapter;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;
   private readonly IMapper _mapper;
   private readonly UserManager<User> _userManager;
   public UserController(IUserService userService, IMapper mapper, UserManager<User> userManager)
   {
       _userService = userService;
       _mapper = mapper;
       _userManager = userManager;
   }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetUserDto>> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        if(user is null) return NotFound();
        return Ok(user);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> UpdateUser(UpdateUserDto? updateUserDto)
    {
        if(updateUserDto == null) return BadRequest();
        updateUserDto.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var updatedUserDto = await _userService.UpdateUser(updateUserDto);
        return Ok(updatedUserDto);
    }

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult<GetUserDto>> PartialUpdateUser(
        [FromBody] JsonPatchDocument<UpdateUserDto> patchDoc)
    {
        var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var patchDto = _mapper.Map<UpdateUserDto>(user);
        patchDoc.ApplyTo(patchDto);
        if (!TryValidateModel(patchDto))
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors });
            return BadRequest(errors);
        }

        var updatedChapterDto = await _userService.PartialUpdateUser(userId, patchDoc); 
        return Ok(updatedChapterDto);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if(await _userService.GetUserById(id) == null) return NotFound();
        await _userService.DeleteUser(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> UpdateUserRole(int id, [FromBody] ChangeRoleDto changeRoleDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if(user is null) return NotFound();
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, changeRoleDto.Role);
        return Ok();
    }
}