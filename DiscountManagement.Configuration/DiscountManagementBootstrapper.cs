using _0_Framework.Infrastructure;
using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Configuration.Permissions;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration;
public class DiscountManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
        services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

        services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
        services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

        services.AddTransient<IPermissionExposer, DiscountPermissionExposer>();

        services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
    }
}