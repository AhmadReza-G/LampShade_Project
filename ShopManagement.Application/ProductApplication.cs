using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopManagement.Application;
public class ProductApplication : IProductApplication
{
    private readonly IProductRepository _productRepository;

    public ProductApplication(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public OperationResult Create(CreateProduct command)
    {
        var operation = new OperationResult();
        if (_productRepository.IsExists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var product = new Product(command.Name, command.Code, command.ShortDescription,
            command.Description, command.Picture,
            command.PictureAlt, command.PictureTitle,
            slug, command.Keywords, command.MetaDescription,
            command.CategoryId);
        _productRepository.Create(product);
        _productRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Edit(EditProduct command)
    {
        var operation = new OperationResult();
        var product = _productRepository.GetBy(command.Id);
        if (product is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_productRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        product.Edit(command.Name, command.Code, command.ShortDescription,
            command.Description, command.Picture,
            command.PictureAlt, command.PictureTitle,
            slug, command.Keywords, command.MetaDescription,
            command.CategoryId);
        _productRepository.SaveChanges();

        return operation.Succeded();
    }

    public EditProduct GetDetails(long id)
    {
        return _productRepository.GetDetails(id);
    }

    public List<ProductViewModel> GetProducts()
    {
        return _productRepository.GetProducts();
    }

    public List<ProductViewModel> Search(ProductSearchModel searchModel)
    {
        return _productRepository.Search(searchModel);
    }
}
