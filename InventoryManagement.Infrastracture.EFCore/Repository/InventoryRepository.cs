using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastracture.EFCore;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastracture.EFCore.Migrations;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastracture.EFCore.Repository;
public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
{
    private readonly InventoryContext _inventoryContext;
    private readonly ShopContext _shopContext;
    private readonly AccountContext _accountContext;

    public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
    {
        _inventoryContext = context;
        _shopContext = shopContext;
        _accountContext = accountContext;
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

    public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
    {
        var accounts = _accountContext.Accounts
            .Select(x => new { x.Id, x.Fullname })
            .ToList();
        var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.Id == inventoryId);
        var operations = inventory.Operations.Select(x => new InventoryOperationViewModel
        {
            Id = x.Id,
            OrderId = x.OrderId,
            Count = x.Count,
            CurrentCount = x.CurrentCount,
            Description = x.Description,
            Operation = x.Operation,
            OperationDate = x.OperationDate.ToFarsi(),
            OperatorId = x.OperatorId
        }).OrderByDescending(x => x.Id).ToList();

        foreach (var operation in operations)
            operation.Operator = accounts.FirstOrDefault(x => x.Id == operation.OperatorId)?.Fullname;

        return operations;
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
                CurrentCount = x.CalculateInventoryCount(),
                CreationDate = x.CreationDate.ToFarsi()
            });

        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);

        if (searchModel.IsInStock)
            query = query.Where(x => !x.IsInStock);


        var inventory = query.OrderByDescending(x => x.Id)
            .ToList();

        inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);
        return inventory;

    }
}
