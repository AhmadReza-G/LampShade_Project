using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application;
public class InventoryApplication : IInventoryApplication
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoryApplication(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public OperationResult Create(CreateInventory command)
    {
        var operation = new OperationResult();
        if (_inventoryRepository.IsExists(x => x.ProductId == command.ProductId))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var inventory = new Inventory(command.ProductId, command.UnitPrice);
        _inventoryRepository.Create(inventory);
        _inventoryRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Edit(EditInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.GetBy(command.Id);
        if (inventory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (_inventoryRepository.IsExists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        inventory.Edit(command.ProductId, command.UnitPrice);
        _inventoryRepository.SaveChanges();

        return operation.Succeded();
    }

    public EditInventory GetDetails(long id)
    {
        return _inventoryRepository.GetDetails(id);
    }

    public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
    {
        return _inventoryRepository.GetOperationLog(inventoryId);
    }

    public OperationResult Increase(IncreaseInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.GetBy(command.InventoryId);
        if (inventory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        const long operatorId = 1;
        inventory.Increase(command.Count, operatorId, command.Description);
        _inventoryRepository.SaveChanges();

        return operation.Succeded();
    }

    public OperationResult Reduce(List<ReduceInventory> command)
    {
        var operation = new OperationResult();
        const long operatorId = 1;
        foreach (var rInventory in command)
        {
            var inventory = _inventoryRepository.Get(rInventory.ProductId);
            inventory?.Reduce(rInventory.Count, operatorId, rInventory.Description, rInventory.OrderId);
        }
        _inventoryRepository.SaveChanges();
        return operation.Succeded();
    }

    public OperationResult Reduce(ReduceInventory command)
    {
        var operation = new OperationResult();
        var inventory = _inventoryRepository.GetBy(command.InventoryId);
        if (inventory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        const long operatorId = 1;
        inventory.Reduce(command.Count, operatorId, command.Description, 0);
        _inventoryRepository.SaveChanges();

        return operation.Succeded();
    }

    public List<InventoryViewModel> Search(InventorySearchModel searchModel)
    {
        return _inventoryRepository.Search(searchModel);
    }
}
