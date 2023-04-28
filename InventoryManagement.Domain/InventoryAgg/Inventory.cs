using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace InventoryManagement.Domain.InventoryAgg;
public class Inventory : EntityBase
{
    public long ProductId { get; private set; }
    public double UnitPrice { get; private set; }
    public bool IsInStock { get; private set; } = false;
    public List<InventoryOperation> Operations { get; private set; }

    public Inventory(long productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
    }
    private long CalculateInventoryCount()
    {
        var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
        var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
        return plus - minus;
    }
    public void Increase(long count, long operatorId, string description)
    {
        var currentCount = CalculateInventoryCount() + count;
        var operation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, Id);
        Operations.Add(operation);
        IsInStock = currentCount > 0;
    }
    public void Reduce(long count, long operatorId, string description, long orderId)
    {
        var currentCount = CalculateInventoryCount() - count;
        var operation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, Id);
        Operations.Add(operation);
        IsInStock = currentCount > 0;
    }
}
public class InventoryOperation
{
    public long Id { get; private set; }
    public bool Operation { get; private set; }
    public long Count { get; private set; }
    public long OperatorId { get; private set; }
    public DateTime OperationDate { get; private set; } = DateTime.Now;
    public long CurrentCount { get; private set; }
    public string? Description { get; private set; }
    public long OrderId { get; private set; }
    public long InventoryId { get; private set; }
    public Inventory Inventory { get; private set; }

    public InventoryOperation(bool operation, long count, long operatorId, long currentCount, string? description, long orderId, long inventoryId)
    {
        Operation = operation;
        Count = count;
        OperatorId = operatorId;
        CurrentCount = currentCount;
        Description = description;
        OrderId = orderId;
        InventoryId = inventoryId;
    }
}