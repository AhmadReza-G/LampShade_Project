using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Comment;
using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastracture.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastracture.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;
public class ProductQuery : IProductQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;
    private readonly CommentContext _commentContext;

    public ProductQuery(InventoryContext inventoryContext, DiscountContext discountContext, ShopContext shopContext, CommentContext commentContext)
    {
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
        _shopContext = shopContext;
        _commentContext = commentContext;
    }

    public ProductQueryModel GetProductDetails(string slug)
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
                Pictures = MapProductPictures(x.ProductPictures)
            }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
        if (product is null)
            return new ProductQueryModel();
        var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
        if (productInventory is not null)
        {
            var price = productInventory.UnitPrice;
            product.Price = price.ToMoney();
            product.DoublePrice = price;
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

        var comments = _commentContext.Comments
            .Where(x => !x.IsCanceled)
            .Where(x => x.IsConfirmed)
            .Where(x => x.Type == ((int)CommentType.Product))
            .Where(x => x.OwnerRecordId == product.Id)
            .Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Message = x.Message,
                CreationDate = x.CreationDate.ToFarsi()
            })
            .OrderByDescending(x => x.Id)
            .ToList();

        product.Comments = comments;

        return product;
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
            .Where(x => x.IsInStock)
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
            })
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .Take(6)
            .ToList();
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
        products = products
            .Where(x => !string.IsNullOrWhiteSpace(x.Price))
            .ToList();
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
                if (discount is null)
                    continue;
                var discountRate = discount.DiscountRate;
                product.DiscountRate = discountRate;
                product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                product.HasDiscount = discountRate > 0;
                var discountAmount = Math.Round((price * discountRate) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }
        }
        return products;
    }

    public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
    {
        var inventory = _inventoryContext.Inventory
            .ToList();
        foreach (var cartItem in cartItems.Where(cartItem =>
                inventory.Any(x => x.ProductId == cartItem.Id && x.IsInStock)))
        {
            var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
            if (itemInventory is not null)
                cartItem.IsInStock = itemInventory.CalculateInventoryCount() >= cartItem.Count;
        }

        return cartItems;
    }
}