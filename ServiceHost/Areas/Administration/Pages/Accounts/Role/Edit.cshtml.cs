using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role;

public class EditModel : PageModel
{
    public EditRole Command;
    public List<SelectListItem> Permissions = new List<SelectListItem>();
    private readonly IRoleApplication _roleApplication;
    private readonly IEnumerable<IPermissionExposer> _exposers;

    public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
    {
        _roleApplication = roleApplication;
        _exposers = exposers;
    }

    public void OnGet(long id)
    {
        Command = _roleApplication.GetDetails(id);
        var permissions = new List<PermissionDto>();
        foreach (var exposer in _exposers)
        {
            var exposedPermissions = exposer.Expose();
            foreach (var (key, value) in exposedPermissions)
            {
                permissions.AddRange(value);
                var group = new SelectListGroup
                {
                    Name = key
                };
                foreach (var permission in value)
                {
                    var item = new SelectListItem(permission.Name, permission.Code.ToString())
                    {
                        Group = group
                    };

                    if (Command.MappedPermissions.Any(x => x.Code == permission.Code))
                        item.Selected = true;

                    Permissions.Add(item);
                }
            }
        }
    }

    public IActionResult OnPost(EditRole command)
    {
        var result = _roleApplication.Edit(command);
        return RedirectToPage("Index");
    }
}