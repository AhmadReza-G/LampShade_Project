using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg;
public class Order(long accountId, double totalAmount,
        double discountAmount, double payAmount/*, string issueTrackingNo*/, int paymentMethod) : EntityBase
{
    public long AccountId { get; private set; } = accountId;
    public double TotalAmount { get; private set; } = totalAmount;
    public int PaymentMethod { get; private set; } = paymentMethod;
    public double DiscountAmount { get; private set; } = discountAmount;
    public double PayAmount { get; private set; } = payAmount;
    public bool IsPaid { get; private set; } = false;
    public bool IsCancelled { get; private set; } = false;
    public string? IssueTrackingNo { get; private set; }/* = issueTrackingNo;*/
    public long RefId { get; private set; } = 0;
    public List<OrderItem> Items { get; private set; } = new();
    public void PaymentSucceeded(long refId)
    {
        IsPaid = true;
        if (refId != 0)
            RefId = refId;
    }
    public void SetIssueTrackingNo(string issueNumber) => IssueTrackingNo = issueNumber;
    public void Cancel() => IsCancelled = true;
    public void Add(OrderItem item)
    {
        Items.Add(item);
    }
}
