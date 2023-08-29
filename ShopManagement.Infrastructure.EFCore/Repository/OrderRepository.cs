using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastracture.EFCore;
using Microsoft.Identity.Client;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;
public class OrderRepository(ShopContext context, AccountContext accountContext)
                            : RepositoryBase<long, Order>(context), IOrderRepository
{
    private readonly ShopContext _context = context;
    private readonly AccountContext _accountContext = accountContext;

    public double GetAmountBy(long id)
    {
        var order = _context.Orders
            .Select(x => new { x.PayAmount, x.Id })
            .FirstOrDefault(x => x.Id == id);
        return order is not null ? order.PayAmount : 0;
    }

    public List<OrderItemViewModel> GetItems(long orderId)
    {
        var products = _context.Products
            .Select(x => new { x.Id, x.Name })
            .ToList();

        var order = _context.Orders
            .FirstOrDefault(x => x.Id == orderId);

        if (order is null)
            return new List<OrderItemViewModel>();


        var items = order.Items.Select(x => new OrderItemViewModel
        {
            Id = x.Id,
            OrderId = x.OrderId,
            Count = x.Count,
            DiscountRate = x.DiscountRate,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice
        }).ToList();
        foreach (var item in items)
            item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;

        return items;
    }

    public List<OrderViewModel> Search(OrderSearchModel searchModel)
    {
        var accounts = _accountContext.Accounts
            .Select(x => new { x.Id, x.Fullname })
            .ToList();
        var query = _context.Orders
            .Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCancelled,
                IsPaid = x.IsPaid,
                PayAmount = x.PayAmount,
                RefId = x.RefId,
                PaymentMethodId = x.PaymentMethod,
                TotalAmount = x.TotalAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                CreationDate = x.CreationDate.ToFarsi()
            });

        query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

        if (searchModel.AccountId > 0)
            query = query.Where(x => x.AccountId == searchModel.AccountId);

        var orders = query.OrderByDescending(x => x.Id).ToList();

        foreach (var order in orders)
        {
            order.AccountFullName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
            order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId)?.Name;
        }
        return orders;
    }
}
