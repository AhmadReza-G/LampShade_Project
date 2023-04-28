using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration;
public class InventoryManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        //services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
    }
}
