using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Domain.CustomerDiscountAgg;
public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
{
    EditCustomerDiscount GetDetails(long id);
    List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
}
