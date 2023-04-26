using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application;
public class CustomerDiscountApplication : ICustomerDiscountApplication
{
    private readonly ICustomerDiscountRepository _customerDiscountRepository;

    public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
    {
        _customerDiscountRepository = customerDiscountRepository;
    }

    public OperationResult Define(DefineCustomerDiscount command)
    {
        var operation = new OperationResult();
        if (_customerDiscountRepository.IsExists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);
        _customerDiscountRepository.Create(customerDiscount);
        _customerDiscountRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Edit(EditCustomerDiscount command)
    {
        var operation = new OperationResult();
        var customerDiscount = _customerDiscountRepository.GetBy(command.Id);
        if (customerDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_customerDiscountRepository.IsExists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        customerDiscount.Edit(command.ProductId, command.DiscountRate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);
        _customerDiscountRepository.SaveChanges();
        return operation.Succeded();
    }

    public EditCustomerDiscount GetDetails(long id)
    {
        return _customerDiscountRepository.GetDetails(id);
    }

    public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
    {
        return _customerDiscountRepository.Search(searchModel);
    }
}
