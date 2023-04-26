using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount;

public class DefineColleagueDiscount
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public List<ProductViewModel> Products { get; set; }
}
