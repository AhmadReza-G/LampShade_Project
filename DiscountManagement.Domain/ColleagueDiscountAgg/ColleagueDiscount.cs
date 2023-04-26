using _0_Framework.Domain;

public class ColleagueDiscount : EntityBase
{
    public long ProductId { get; private set; }
    public int DiscountRate { get; private set; }
    public bool IsRemoved { get; private set; } = false;

    public ColleagueDiscount(long productId, int discountRate)
    {
        ProductId = productId;
        DiscountRate = discountRate;
    }
    public void Edit(long productId, int discountRate)
    {
        ProductId = productId;
        DiscountRate = discountRate;
    }
    public void Remove()
    {
        IsRemoved = true;
    }
    public void Restore()
    {
        IsRemoved = false;
    }
}
