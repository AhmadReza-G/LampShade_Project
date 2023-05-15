using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.ArticleCategory;
public class CreateArticleCategory
{
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
    [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile? Picture { get; set; }

    public string? PictureAlt { get; set; }
    public string? PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Slug { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Keywords { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string MetaDescription { get; set; }


    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public int ShowOrder { get; set; }

    public string? CanonicalAddress { get; set; }
}