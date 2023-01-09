using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebTruyenChu_Backend.DTOs;

public class UpdateAuthorDto
{
    [System.Text.Json.Serialization.JsonIgnore] 
    public int AuthorId { get; set; }
    [MaxLength(256, ErrorMessage = "Author name can't be longer than 256 characters")]
    [Required(ErrorMessage = "AuthorName is required")]
    public string? AuthorName { get; set; }}