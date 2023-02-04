using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Data;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Services.Interfaces;

namespace WebTruyenChu_Backend.Services;

public class UserService : IUserService
{
    private readonly WebTruyenChuContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserService(WebTruyenChuContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<GetUserDto?> GetUserById(int id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        return _mapper.Map<GetUserDto>(user);
    }

    public async Task<GetUserDto> UpdateUser(UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateUserDto.UserId);
        _mapper.Map(updateUserDto, user);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetUserDto>(user);
    }

    public async Task<GetUserDto> PartialUpdateUser(int userId, JsonPatchDocument<UpdateUserDto> patchDoc)
    {
        var user = await _context.Users.FindAsync(userId);
        var userToPatch = _mapper.Map<UpdateUserDto>(user);
        patchDoc.ApplyTo(userToPatch);
        _mapper.Map(userToPatch, user);
        await _context.SaveChangesAsync();
        return _mapper.Map<GetUserDto>(user);
    }

    public async Task DeleteUser(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var logins = await _userManager.GetLoginsAsync(user);
        var rolesForUser = await _userManager.GetRolesAsync(user);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        IdentityResult result = IdentityResult.Success;
        foreach (var login in logins)
        {
            result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
            if (result != IdentityResult.Success)
                break;
        }

        if (result == IdentityResult.Success)
        {
            foreach (var item in rolesForUser)
            {
                result = await _userManager.RemoveFromRoleAsync(user, item);
                if (result != IdentityResult.Success)
                    break;
            }
        }

        if (result == IdentityResult.Success)
        {
            result = await _userManager.DeleteAsync(user);
            if (result == IdentityResult.Success)
                await transaction.CommitAsync(); //only commit if user and all his logins/roles have been deleted  
        }
    }
}