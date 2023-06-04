using _0_Framework.Infrastructure;

namespace InventoryManagement.Configuration.Permissions;
public class InventoryPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        ["انبار"] = new List<PermissionDto>()
        {
            new(InventoryPermissions.CreateInventory, "ایجاد انبار"),
            new(InventoryPermissions.EditInventory, "ویرایش انبار"),
            new(InventoryPermissions.ListInventory, "لیست انبارها"),
            new(InventoryPermissions.SearchInventory, "جستجوی انبارها"),
            new(InventoryPermissions.Reduce, "کاهش موجودی"),
            new(InventoryPermissions.Increase, "افزایش موجودی"),
            new(InventoryPermissions.OperationLog, "گزارش عملیات")
        }
    };
}