using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository;
public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
{
    private readonly ShopContext _context;

    public ProductPictureRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProductPicture GetDetails(long id)
    {
        return _context.ProductPictures.Select(x => new EditProductPicture
        {
            Id = x.Id,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ProductId = x.ProductId
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
    {
        var query = _context.ProductPictures.Include(x => x.Product)
            .Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString(),
                Product = x.Product.Name,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
            });

        if (searchModel.ProductId != 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);

        return query.OrderByDescending(x => x.Id).ToList();
    }
}