using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Query;
using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastracture.EFCore;
using InventoryManagement.Infrastracture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration;
public class InventoryManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IInventoryApplication, InventoryApplication>();
        services.AddTransient<IInventoryRepository, InventoryRepository>();

        services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

        services.AddTransient<IInventoryQuery, InventoryQuery>();

        services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
    }
}
