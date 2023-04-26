using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository;
public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProduct GetDetails(long id)
    {
        return _context.Products.Select(x => new EditProduct
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            ShortDescription = x.ShortDescription,
            Description = x.Description,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Keywords = x.Keywords,
            Slug = x.Slug,
            MetaDescription = x.MetaDescription,
            UnitPrice = x.UnitPrice,
            CategoryId = x.CategoryId
        }).FirstOrDefault(x => x.Id == id);
    }
    public List<ProductViewModel> GetProducts()
    {
        return _context.Products.Select(x => new ProductViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
    public List<ProductViewModel> Search(ProductSearchModel searchModel)
    {
        var query = _context.Products.Include(x => x.Category)
            .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                Code = x.Code,
                CategoryId = x.CategoryId,
                Category = x.Category.Name,
                UnitPrice = x.UnitPrice,
                CreationDate = x.CreationDate.ToFarsi(),
                IsInStock = x.IsInStock
            });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));

        if (!string.IsNullOrWhiteSpace(searchModel.Code))
            query = query.Where(x => x.Code.Contains(searchModel.Code));

        if (searchModel.CategoryId != 0)
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);

        return query.OrderByDescending(x => x.Id).ToList();
    }
}
