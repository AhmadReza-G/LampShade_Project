using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions;
public class ShopPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        ["محصول"] = new List<PermissionDto>
        {
            new(ShopPermissions.CreateProduct, "ایجاد محصول"),
            new(ShopPermissions.EditProduct, "ویرایش محصول"),
            new(ShopPermissions.ListProducts, "لیست محصولات"),
            new(ShopPermissions.SearchProducts, "جستجوی محصول")
        },

        ["گروه محصول"] = new List<PermissionDto>
        {
            new(ShopPermissions.CreateProductCategory, "ایجاد گروه محصول"),
            new(ShopPermissions.EditProductCategory, "ویرایش گروه محصول"),
            new(ShopPermissions.ListProductCategories, "لیست گروه محصولات"),
            new(ShopPermissions.SearchProductCategories, "جستجوی گروه محصولات")
        },
        ["عکس محصول"] = new List<PermissionDto>
        {
            new(ShopPermissions.CreateProductPicture, "ایجاد عکس محصول"),
            new(ShopPermissions.EditProductPicture, "ویرایش عکس محصول"),
            new(ShopPermissions.SearchProductPictures, "جستجوی عکس محصول"),
            new(ShopPermissions.RemoveProductPicture, "حذف عکس محصول"),
            new(ShopPermissions.RestoreProductPicture, "بازیابی عکس محصول")
        },
        ["اسلایدر"] = new List<PermissionDto>
        {
            new(ShopPermissions.CreateSlide, "ایجاد اسلایدر"),
            new(ShopPermissions.EditSlide, "ویرایش اسلایدر"),
            new(ShopPermissions.ListSlides, "لیست اسلایدرها"),
            new(ShopPermissions.RemoveSlide, "حذف اسلایدر"),
            new(ShopPermissions.RestoreSlide, "بازیابی اسلایدر")
        },
        ["سفارشات"] = new List<PermissionDto>
        {
            new(ShopPermissions.SearchOrders, "جستجوی سفارش"),
            new(ShopPermissions.ListOrders, "لیست سفارشات"),
            new(ShopPermissions.ConfirmOrder, "تأیید سفارش"),
            new(ShopPermissions.CancelOrder, "رد سفارش")
        }
    };
}
