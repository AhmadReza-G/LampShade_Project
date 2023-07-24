using ShopManagement.Application.Contracts.Order;

namespace _01_LampshadeQuery.Contracts.Product;
public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
    ProductQueryModel GetProductDetails(string slug);
    List<ProductQueryModel> Search(string value);
    List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
}