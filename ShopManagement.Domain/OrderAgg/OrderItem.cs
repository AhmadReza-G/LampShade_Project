using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg;

public class OrderItem(long productId, int count,
    double unitPrice, int discountRate) : EntityBase
{
    public long ProductId { get; private set; } = productId;
    public int Count { get; private set; } = count;
    public double UnitPrice { get; private set; } = unitPrice;
    public int DiscountRate { get; private set; } = discountRate;
    public long OrderId { get; private set; }
    public Order Order { get; private set; }
}