using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount;

public class DefineColleagueDiscount
{
    [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
    public long ProductId { get; set; }
    [Range(minimum: 1, 99, ErrorMessage = ValidationMessages.IsRequired)]
    public int DiscountRate { get; set; }
    public List<ProductViewModel> Products { get; set; }
}
