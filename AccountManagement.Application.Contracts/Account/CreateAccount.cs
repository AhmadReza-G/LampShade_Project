using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account;
public class CreateAccount
{
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Fullname { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Username { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Password { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
    public long RoleId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Mobile { get; set; }

    [FileExtentionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
    [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile? ProfilePhoto { get; set; }

    public List<RoleViewModel> Roles { get; set; }
}