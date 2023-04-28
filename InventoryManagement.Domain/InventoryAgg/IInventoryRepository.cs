using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg;
public interface IInventoryRepository : IRepository<long, Inventory>
{
    EditInventory GetDetails(long id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    Inventory Get(long productId);
}