using _0_Framework.Domain;

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
    public void Edit(long productId, double unitPrice)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
    }
    public long CalculateInventoryCount()
    {
        var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
        var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
        return plus - minus;
    }
    public void Increase(long count, long operatorId, string? description)
    {
        var currentCount = CalculateInventoryCount() + count;
        var operation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, Id);
        Operations.Add(operation);
        IsInStock = currentCount > 0;
    }
    public void Reduce(long count, long operatorId, string? description, long orderId)
    {
        var currentCount = CalculateInventoryCount() - count;
        var operation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, Id);
        Operations.Add(operation);
        IsInStock = currentCount > 0;
    }
}