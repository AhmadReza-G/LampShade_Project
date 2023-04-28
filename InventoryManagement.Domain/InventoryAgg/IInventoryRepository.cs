using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.InventoryAgg;
public interface IInventoryRepository : IRepository<long, Inventory>
{
    EditInventory GetDetails(long id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    Inventory GetBy(long productId);
}
