using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory;
public interface IProductCategoryApplication
{
    OperationResult Create(CreateProductCategory command);
    OperationResult Edit(EditProductCategory command);
    List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    EditProductCategory GetDetails(long id);
    List<ProductCategoryViewModel> GetProductCategories();

}
