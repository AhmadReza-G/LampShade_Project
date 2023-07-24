using _0_Framework.Infrastructure;

namespace DiscountManagement.Configuration.Permissions;
public class DiscountPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        ["تخفیف همکار"] = new List<PermissionDto>
        {
            new(DiscountPermissions.DefineColleagueDiscount, "تعریف تخفیف همکار"),
            new(DiscountPermissions.EditColleagueDiscount, "ویرایش تخفیف همکار"),
            new(DiscountPermissions.SearchColleagueDiscounts, "جستجوی تخفیف‌های همکار"),
            new(DiscountPermissions.RemoveColleagueDiscount, "حذف تخفیف همکار"),
            new(DiscountPermissions.ActivateColleagueDiscount, "فعال‌سازی تخفیف همکار"),
            new(DiscountPermissions.DeactivateColleagueDiscount, "غیرفعال‌سازی تخفیف همکار"),
        },

        ["تخفیف مشتری"] = new List<PermissionDto>
        {
            new(DiscountPermissions.DefineCustomerDiscount, "تعریف تخفیف مشتری"),
            new(DiscountPermissions.EditCustomerDiscount, "ویرایش تخفیف مشتری"),
            new(DiscountPermissions.SearchCustomerDiscounts, "جستجوی تخفیف‌های مشتری")
        }
    };
}