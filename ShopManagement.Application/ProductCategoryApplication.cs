using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application;
public class ProductCategoryApplication : IProductCategoryApplication
{
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }

    public OperationResult Create(CreateProductCategory command)
    {
        var operation = new OperationResult();
        if (_productCategoryRepository.IsExists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
            command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
        _productCategoryRepository.Create(productCategory);
        _productCategoryRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        var operation = new OperationResult();
        var productCategory = _productCategoryRepository.GetBy(command.Id);
        if (productCategory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_productCategoryRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        productCategory.Edit(command.Name, command.Description, command.Picture,
            command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
        _productCategoryRepository.SaveChanges();

        return operation.Succeded();
    }

    public EditProductCategory GetDetails(long id)
    {
        return _productCategoryRepository.GetDetails(id);
    }

    public List<ProductCategoryViewModel> GetProductCategories()
    {
        return _productCategoryRepository.GetProductCategories();
    }

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
    {
        return _productCategoryRepository.Search(searchModel);
    }
}
