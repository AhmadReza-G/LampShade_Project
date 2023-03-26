using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductPicture;
public interface IProductPictureApplication
{
    OperationResult Create(CreateProductPicture command);
    OperationResult Edit(EditProductPicture command);
    List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    EditProductPicture GetDetails(long id);
    OperationResult Remove(long id);
    OperationResult Restore(long id);
}
