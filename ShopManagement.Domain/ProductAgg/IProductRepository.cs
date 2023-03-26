using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductAgg;
public interface IProductRepository : IRepository<long, Product>
{
    EditProduct GetDetails(long id);
    public List<ProductViewModel> GetProducts();
    List<ProductViewModel> Search(ProductSearchModel searchModel);
}
