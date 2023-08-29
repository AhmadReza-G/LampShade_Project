using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Infrastracture.EFCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;
public class InventoryQuery : IInventoryQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;

    public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
    {
        _inventoryContext = inventoryContext;
        _shopContext = shopContext;
    }

    public StockStatus CheckStock(IsInStock command)
    {
        var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);
        if (inventory == null || inventory.CalculateInventoryCount() < command.Count)
        {
            var product = _shopContext.Products
                .Select(x => new { x.Id, x.Name })
                .FirstOrDefault(x => x.Id == command.ProductId);
            return new StockStatus
            {
                IsInStock = false,
                ProductName = product?.Name
            };
        }

        return new StockStatus
        {
            IsInStock = true
        };
    }
}
