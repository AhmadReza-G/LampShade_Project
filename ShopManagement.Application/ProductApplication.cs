﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application;
public class ProductApplication : IProductApplication
{
    private readonly IFileUploader _fileUploader;
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
    {
        _productRepository = productRepository;
        _fileUploader = fileUploader;
        _productCategoryRepository = productCategoryRepository;
    }

    public OperationResult Create(CreateProduct command)
    {
        var operation = new OperationResult();
        if (_productRepository.IsExists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var picturePath = $"{_productCategoryRepository.GetSlugBy(command.CategoryId)}/{slug}";
        var fileName = _fileUploader.Upload(command.Picture, picturePath);
        var product = new Product(command.Name, command.Code, command.ShortDescription,
            command.Description, fileName,
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
        var product = _productRepository.GetProductWithCategory(command.Id);
        if (product is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_productRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var picturePath = $"{product.Category.Slug}/{slug}";
        var fileName = _fileUploader.Upload(command.Picture, picturePath);
        product.Edit(command.Name, command.Code, command.ShortDescription,
            command.Description, fileName,
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
