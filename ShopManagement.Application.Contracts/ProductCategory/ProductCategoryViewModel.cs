namespace ShopManagement.Application.Contracts.ProductCategory;

public class ProductCategoryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Picture { get; set; }
    public string CreationDate { get; set; }
    //public required long ProductsCount { get; set; } = 0;
}