using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application;
public class ProductPictureApplication : IProductPictureApplication
{
    private readonly IProductPictureRepository _productPictureRepository;

    public ProductPictureApplication(IProductPictureRepository productPictureRepository)
    {
        _productPictureRepository = productPictureRepository;
    }

    public OperationResult Create(CreateProductPicture command)
    {
        var operation = new OperationResult();
        if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var productPicture = new ProductPicture(command.ProductId,
            command.Picture, command.PictureAlt,
            command.PictureTitle);
        _productPictureRepository.Create(productPicture);
        _productPictureRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Edit(EditProductPicture command)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.GetBy(command.Id);
        if (productPicture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        productPicture.Edit(command.ProductId,
            command.Picture, command.PictureAlt,
            command.PictureTitle);
        _productPictureRepository.SaveChanges();

        return operation.Succeded();
    }

    public EditProductPicture GetDetails(long id)
    {
        return _productPictureRepository.GetDetails(id);
    }

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.GetBy(id);
        if (productPicture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        productPicture.Remove();
        _productPictureRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.GetBy(id);
        if (productPicture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        productPicture.Restore();
        _productPictureRepository.SaveChanges();
        return operation.Succeded();

    }

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
    {
        return _productPictureRepository.Search(searchModel);
    }
}
