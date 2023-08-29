namespace ShopManagement.Application.Contracts;

public class PaymentMethod(int id, string name, string description)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public static List<PaymentMethod> GetList() => new List<PaymentMethod>
        {
            new(1, "پرداخت اینترنتی",
                "در مدل شما به درگاه پرداخت اینترنتی هدایت شده و درلحظه پرداخت وجه را انجام خواهید داد."),
            new(2, "پرداخت نقدی",
                "در این مدل، ابتدا سفارش ثبت شده و سپس وجه به صورت نقدی در زمان تحویل کالا، دریافت خواهد شد.")
        };

    public static PaymentMethod? GetBy(long id) => GetList().FirstOrDefault(x => x.Id == id);
}