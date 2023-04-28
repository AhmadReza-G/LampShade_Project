using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastracture.EFCore.Repository;
public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
{
    private readonly InventoryContext _inventoryContext;
    private readonly ShopContext _shopContext;

    public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
    {
        _inventoryContext = context;
        _shopContext = shopContext;
    }

    public Inventory Get(long productId)
    {
        return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
    }

    public EditInventory GetDetails(long id)
    {
        return _inventoryContext.Inventory.Select(x => new EditInventory
        {
            Id = id,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        var products = _shopContext.Products
            .Select(x => new { x.Id, x.Name })
            .ToList();
        var query = _inventoryContext.Inventory
            .Select(x => new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                IsInStock = x.IsInStock,
                CurrentCount = x.CalculateInventoryCount()
            });

        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);

        if (!searchModel.IsInStock)
            query = query.Where(x => !x.IsInStock);


        var inventory = query.OrderByDescending(x => x.Id)
            .ToList();

        inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);
        return inventory;

    }
}
