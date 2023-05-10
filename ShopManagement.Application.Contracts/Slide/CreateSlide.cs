using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Slide;
public class CreateSlide
{
    [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
    [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Heading { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Title { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Text { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string BtnText { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Link { get; set; }
}