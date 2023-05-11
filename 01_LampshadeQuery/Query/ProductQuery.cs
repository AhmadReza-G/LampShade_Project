using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastracture.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;
using System.Security.Cryptography.X509Certificates;

namespace _01_LampshadeQuery.Query;
public class ProductQuery : IProductQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;

    public ProductQuery(InventoryContext inventoryContext, DiscountContext discountContext, ShopContext shopContext)
    {
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
        _shopContext = shopContext;
    }

    public ProductQueryModel GetDetails(string slug)
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice, x.IsInStock })
            .ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
            .ToList();
        var product = _shopContext.Products
            .Include(x => x.Category)
            .Include(x => x.Comments)
            .Include(x => x.ProductPictures)
            .Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Name = x.Name,
                PictureTitle = x.PictureTitle,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                Slug = x.Slug,
                Code = x.Code,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords,
                ShortDescription = x.ShortDescription,
                CategorySlug = x.Category.Slug,
                Pictures = MapProductPictures(x.ProductPictures),
                Comments = MapComments(x.Comments)
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
        if (product is null)
            return new ProductQueryModel();
        var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
        if (productInventory is not null)
        {
            var price = productInventory.UnitPrice;
            product.Price = price.ToMoney();
            product.IsInStock = productInventory.IsInStock;
            var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
            if (discount is not null)
            {
                var discountRate = discount.DiscountRate;
                product.DiscountRate = discountRate;
                product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                product.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round((price * discountRate) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }
        }
        return product;
    }

    private static List<CommentQueryModel> MapComments(List<Comment> comments)
    {
        return comments.Where(x => !x.IsCanceled)
            .Where(x => x.IsConfirmed)
            .Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Message = x.Message,
                Name = x.Name
            }).OrderByDescending(x => x.Id)
            .ToList();
    }

    private static List<ProductPicturesQueryModel> MapProductPictures(List<ProductPicture> productPictures)
    {
        return productPictures.Select(x => new ProductPicturesQueryModel
        {
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ProductId = x.ProductId,
            IsRemoved = x.IsRemoved
        }).Where(x => !x.IsRemoved)
        .ToList();
    }

    public List<ProductQueryModel> GetLatestArrivals()
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice })
            .ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate })
            .ToList();
        var products = _shopContext.Products
            .Include(x => x.Category)
            .Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Name = x.Name,
                PictureTitle = x.PictureTitle,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                Slug = x.Slug,
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();
        foreach (var product in products)
        {
            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory is not null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount is not null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }
        return products;
    }

    public List<ProductQueryModel> Search(string value)
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new { x.ProductId, x.UnitPrice, x.IsInStock })
            .ToList();

        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate });

        var query = _shopContext.Products
            .Include(x => x.Category)
            .Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                Category = product.Category.Name,
                CategorySlug = product.Category.Slug,
                ShortDescription = product.ShortDescription
            }).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(value))
            query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
        var products = query.OrderByDescending(x => x.Id).ToList();
        foreach (var product in products)
        {
            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory is not null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.IsInStock = productInventory.IsInStock;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount is not null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }

        return products;
    }
}
