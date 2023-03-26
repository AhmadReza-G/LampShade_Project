using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Product;
public interface IProductApplication
{
    OperationResult Create(CreateProduct command);
    OperationResult Edit(EditProduct command);
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    EditProduct GetDetails(long id);
    OperationResult InStock(long id);
    OperationResult NotInStock(long id);
    List<ProductViewModel> GetProducts();
}