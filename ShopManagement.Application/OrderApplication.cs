using _0_Framework.Application;
using _0_Framework.Application.Sms;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application;
public class OrderApplication(IOrderRepository orderRepository,
    IAuthHelper authHelper, IConfiguration configuration,
    IShopInventoryAcl shopInventoryAcl, ISmsService smsService,
    IShopAccountAcl shopAccountAcl) : IOrderApplication
{
    private readonly IAuthHelper _authHelper = authHelper;
    private readonly ISmsService _smsService = smsService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IShopAccountAcl _shopAccountAcl = shopAccountAcl;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IShopInventoryAcl _shopInventoryAcl = shopInventoryAcl;


    public double GetAmountBy(long id)
    {
        return _orderRepository.GetAmountBy(id);
    }

    public void Cancel(long id)
    {
        var order = _orderRepository.GetBy(id);
        order?.Cancel();
        _orderRepository.SaveChanges();
    }
    public string PaymentSucceeded(long orderId, long refId)
    {
        var order = _orderRepository.GetBy(orderId);
        order.PaymentSucceeded(refId);
        var symbol = _configuration.GetValue<string>("Symbol");
        var issueTrackingNo = CodeGenerator.Generate(symbol);
        order.SetIssueTrackingNo(issueTrackingNo);
        if (!_shopInventoryAcl.ReduceFromInventory(order.Items)) return "";

        _orderRepository.SaveChanges();
        var (name, mobile) = _shopAccountAcl.GetAccountBy(order.AccountId);
        _smsService.Send(mobile ?? string.Empty,
            $"کاربر {name ?? "گرامی"} سفارش شما با شماره پیگیری {issueTrackingNo}" +
            $" با موفقیت  پرداخت شد و ارسال خواهد شد.");

        return issueTrackingNo;
    }

    public long PlaceOrder(Cart cart)
    {
        var userId = _authHelper.CurrentAccountId();
        var order = new Order(userId, cart.TotalAmount,
            cart.DiscountAmount, cart.PayAmount, cart.PaymentMethod);
        foreach (var cartItem in cart.Items)
        {
            var orderItem = new OrderItem(cartItem.Id, cartItem.Count,
                cartItem.UnitPrice, cartItem.DiscountRate);

            order.Add(orderItem);
        }

        _orderRepository.Create(order);
        _orderRepository.SaveChanges();
        return order.Id;
    }

    public List<OrderViewModel> Search(OrderSearchModel searchModel)
    {
        return _orderRepository.Search(searchModel);
    }

    public List<OrderItemViewModel> GetItems(long orderId)
    {
        return _orderRepository.GetItems(orderId);
    }
}
