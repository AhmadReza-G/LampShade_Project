namespace ShopManagement.Application.Contracts.ProductCategory;

public class ProductCategoryViewModel
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public string? Picture { get; set; }
    public required string CreationDate { get; set; }
    //public required long ProductsCount { get; set; } = 0;
}