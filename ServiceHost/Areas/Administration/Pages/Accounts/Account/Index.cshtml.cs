using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account;


[Authorize(Roles = _0_Framework.Infrastructure.Roles.Administrator)]
public class IndexModel : PageModel
{
    [TempData]
    public string Message { get; set; }
    public AccountSearchModel SearchModel;
    public List<AccountViewModel> Accounts;
    public SelectList Roles;

    private readonly IRoleApplication _roleApplication;
    private readonly IAccountApplication _accountApplication;

    public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
    {
        _roleApplication = roleApplication;
        _accountApplication = accountApplication;
    }
    public void OnGet(AccountSearchModel searchModel)
    {
        Roles = new SelectList(_roleApplication.List(), "Id", "Name");
        Accounts = _accountApplication.Search(searchModel);
    }

    public IActionResult OnGetRegister()
    {
        var command = new RegisterAccount
        {
            Roles = _roleApplication.List()
        };
        return Partial("./Register", command);
    }
    [NeedsPermission(AccountPermissions.RegisterAccount)]
    public JsonResult OnPostRegister(RegisterAccount command)
    {
        var result = _accountApplication.Register(command);
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var account = _accountApplication.GetDetails(id);
        account.Roles = _roleApplication.List();
        return Partial("Edit", account);
    }
    [NeedsPermission(AccountPermissions.EditAccount)]
    public JsonResult OnPostEdit(EditAccount command)
    {
        var result = _accountApplication.Edit(command);
        return new JsonResult(result);
    }

    public IActionResult OnGetChangePassword(long id)
    {
        var command = new ChangePassword { Id = id };
        return Partial("ChangePassword", command);
    }
    [NeedsPermission(AccountPermissions.ChangePassword)]
    public JsonResult OnPostChangePassword(ChangePassword command)
    {
        var result = _accountApplication.ChangePassword(command);
        return new JsonResult(result);
    }
}
