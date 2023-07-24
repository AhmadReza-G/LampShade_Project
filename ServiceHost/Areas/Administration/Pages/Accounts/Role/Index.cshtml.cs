using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role;

[Authorize(Roles = _0_Framework.Infrastructure.Roles.Administrator)]
public class IndexModel : PageModel
{
    public List<RoleViewModel> Roles;

    private readonly IRoleApplication _roleApplication;

    public IndexModel(IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
    }
    [NeedsPermission(AccountPermissions.ListRoles)]
    public void OnGet()
    {
        Roles = _roleApplication.List();
    }
}
