using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPicture;
public class CreateProductPicture
{
    [Range(0, 100000, ErrorMessage = ValidationMessages.IsRequired)]
    public long ProductId { get; set; }

    //[Required(ErrorMessage = ValidationMessages.IsRequired)]
    [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
    [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile Picture { get; set; }
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureAlt { get; set; }
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureTitle { get; set; }

    public List<ProductViewModel> Products { get; set; }
}