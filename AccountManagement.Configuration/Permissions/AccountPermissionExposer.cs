using _0_Framework.Infrastructure;

namespace AccountManagement.Configuration.Permissions;
public class AccountPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        ["حساب کاربری"] = new List<PermissionDto>
        {
            new(AccountPermissions.RegisterAccount, "ایجاد حساب"),
            new(AccountPermissions.EditAccount, "ویرایش حساب"),
            new(AccountPermissions.SearchAccounts, "جستجوی حساب‌ها"),
            new(AccountPermissions.ChangePassword, "تغییر رمز")
        },
        ["نقش"] = new List<PermissionDto>
        {
            new(AccountPermissions.CreateRole, "ایجاد نقش"),
            new(AccountPermissions.EditRole, "ویرایش نقش"),
            new(AccountPermissions.ListRoles, "لیست نقش‌ها")
        }
    };
}
