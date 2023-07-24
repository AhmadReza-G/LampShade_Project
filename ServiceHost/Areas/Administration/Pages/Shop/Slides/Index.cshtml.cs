using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides;

[Authorize(Roles = Roles.Administrator)]
public class IndexModel : PageModel
{
    [TempData]
    public string Message { get; set; }
    public List<SlideViewModel> Slides;

    private readonly ISlideApplication _slideApplication;

    public IndexModel(ISlideApplication slideApplication)
    {
        _slideApplication = slideApplication;
    }
    [NeedsPermission(ShopPermissions.ListSlides)]
    public void OnGet()
    {
        Slides = _slideApplication.GetList();
    }

    public IActionResult OnGetCreate()
    {
        var command = new CreateSlide();
        return Partial("./Create", command);
    }
    [NeedsPermission(ShopPermissions.CreateSlide)]
    public JsonResult OnPostCreate(CreateSlide command)
    {
        var result = _slideApplication.Create(command);
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var slide = _slideApplication.GetDetails(id);
        return Partial("Edit", slide);
    }
    [NeedsPermission(ShopPermissions.EditSlide)]
    public JsonResult OnPostEdit(EditSlide command)
    {
        var result = _slideApplication.Edit(command);
        return new JsonResult(result);
    }
    [NeedsPermission(ShopPermissions.RemoveSlide)]
    public IActionResult OnGetRemove(long id)
    {
        var result = _slideApplication.Remove(id);
        if (result.IsSucceded)
            return RedirectToPage("./Index");

        Message = result.Message;
        return RedirectToPage("./Index");
    }
    [NeedsPermission(ShopPermissions.RestoreSlide)]
    public IActionResult OnGetRestore(long id)
    {
        var result = _slideApplication.Restore(id);
        if (result.IsSucceded)
            return RedirectToPage("./Index");

        Message = result.Message;
        return RedirectToPage("./Index");
    }
}
