namespace ShopManagement.Configuration.Permissions;
public static class ShopPermissions
{
    // Product Permissions
    public const int CreateProduct = 100;
    public const int EditProduct = 101;
    public const int ListProducts = 102;
    public const int SearchProducts = 103;

    // ProductCategory Permissions
    public const int CreateProductCategory = 110;
    public const int EditProductCategory = 111;
    public const int ListProductCategories = 112;
    public const int SearchProductCategories = 113;

    // ProductPicture Permissions
    public const int CreateProductPicture = 120;
    public const int EditProductPicture = 121;
    public const int SearchProductPictures = 122;
    public const int RemoveProductPicture = 123;
    public const int RestoreProductPicture = 124;

    // Slider Permissions
    public const int CreateSlide = 130;
    public const int EditSlide = 131;
    public const int ListSlides = 132;
    public const int RemoveSlide = 133;
    public const int RestoreSlide = 134;
}
