namespace _01_LampshadeQuery.Contracts.Product;

public class ProductPicturesQueryModel
{
    public long ProductId { get; set; }
    public string? Picture { get; set; }
    public string? PictureAlt { get; set; }
    public string? PictureTitle { get; set; }
    public bool IsRemoved { get; set; }
}