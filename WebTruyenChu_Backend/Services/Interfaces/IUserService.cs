using Microsoft.AspNetCore.JsonPatch;
using WebTruyenChu_Backend.DTOs;

namespace WebTruyenChu_Backend.Services.Interfaces;

public interface IUserService
{
    Task<GetUserDto?> GetUserById(int id);
    Task<GetUserDto> UpdateUser(UpdateUserDto updateUserDto);
    Task<GetUserDto> PartialUpdateUser(int userId, JsonPatchDocument<UpdateUserDto> patchDoc);
    Task DeleteUser(int userId);
}