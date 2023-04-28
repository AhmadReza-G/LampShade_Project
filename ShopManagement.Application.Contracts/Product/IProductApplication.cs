using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product;
public interface IProductApplication
{
    OperationResult Create(CreateProduct command);
    OperationResult Edit(EditProduct command);
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    EditProduct GetDetails(long id);
    List<ProductViewModel> GetProducts();
}