using _01_LampshadeQuery.Contracts.Product;

namespace _01_LampshadeQuery.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    List<ProductCategoryQueryModel> GetProductCategories();
    List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    ProductCategoryQueryModel GetProductCategoryWithProducstsBy(string slug);
}
