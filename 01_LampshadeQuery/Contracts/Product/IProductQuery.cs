namespace _01_LampshadeQuery.Contracts.Product;
public interface IProductQuery
{
    List<ProductQueryModel> GetLatestArrivals();
    ProductQueryModel GetProductDetails(string slug);
    List<ProductQueryModel> Search(string value);
}
