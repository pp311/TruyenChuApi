using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using WebTruyenChu_Backend.Constants;
using WebTruyenChu_Backend.DTOs;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Extensions;
using WebTruyenChu_Backend.Helper;

namespace WebTruyenChu_Backend.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<User> userManager, IMapper mapper, IConfiguration configuration, IEmailSender emailSender)
    {
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
        _emailSender = emailSender;
        _jwtSettings = _configuration.GetSection("JwtSettings");
    }
   
    [HttpPost("registration")] 
    public async Task<IActionResult> RegisterUser([FromBody] AddUserDto? addUserDto) 
    {
        if (addUserDto == null || !ModelState.IsValid) 
            return BadRequest(); 
            
        var user = _mapper.Map<User>(addUserDto);
        var result = await _userManager.CreateAsync(user, addUserDto.Password); 
        if (!result.Succeeded) 
        { 
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(errors); 
        }
        //var createdUser = await _userManager.FindByNameAsync(user.UserName);
        await _userManager.AddToRoleAsync(user, Role.Member);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(await _userManager.FindByEmailAsync(user.Email));
        //khi aspnet parse token fromquery thi "+" -> " " nen can encode (hoac chuyen method cua email confirmation sang POST)
        var encodedToken = token.Base64ForUrlEncode();
        var message = new Message(new string[] { user.Email }, "Email confirmation token", encodedToken);
        await _emailSender.SendEmailAsync(message);
        return StatusCode(201); 
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        User user;
        //kiem tra username co phai la email
        try
        { 
            var emailAddress = new MailAddress(loginDto.UserName);
            user = await _userManager.FindByEmailAsync(loginDto.UserName);
        }
        catch
        {
            user = await _userManager.FindByNameAsync(loginDto.UserName);
        }

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized(new { ErrorMessage = "Invalid Authentication" });
        } 
        
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.First();
        
        var tokenOptions = new JwtSecurityToken(
        issuer: _jwtSettings["validIssuer"],
        audience: _jwtSettings["validAudience"],
        claims: new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name ?? ""),
            new(ClaimTypes.Role, role) 
        },
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
        signingCredentials: signingCredentials);
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new { Token = tokenString});
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user is null) return BadRequest();
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //var param = new Dictionary<string, string?>
        //{
        //    {"token", token },
        //    {"email", forgotPasswordDto.Email }
        //};
        var message = new Message(new string[] { user.Email }, "Reset password token", token);
        await _emailSender.SendEmailAsync(message);
                
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
            return BadRequest();
        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (resetPassResult.Succeeded) return Ok();
        
        var errors = resetPassResult.Errors.Select(e => e.Description);
        return BadRequest(new { Errors = errors });
    }
    
    [HttpGet("email-confirmation")]
    public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return BadRequest();
        var confirmResult = await _userManager.ConfirmEmailAsync(user, token.Base64ForUrlDecode());
        if (!confirmResult.Succeeded)
            return BadRequest();
        return Ok();
}
}