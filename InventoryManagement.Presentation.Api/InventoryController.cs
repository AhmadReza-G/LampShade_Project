﻿using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using _01_LampshadeQuery.Contracts.Inventory;

namespace InventoryManagement.Presentation.Api;
[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryQuery _inventoryQuery;
    private readonly IInventoryApplication _inventoryApplication;

    public InventoryController(IInventoryQuery inventoryQuery, IInventoryApplication inventoryApplication)
    {
        _inventoryQuery = inventoryQuery;
        _inventoryApplication = inventoryApplication;
    }

    [HttpGet("{id}")]
    public List<InventoryOperationViewModel> GetOperationsBy(long id)
    {
        return _inventoryApplication.GetOperationLog(id);
    }

    [HttpPost]
    public StockStatus CheckStock(IsInStock command)
    {
        return _inventoryQuery.CheckStock(command);
    }
}
