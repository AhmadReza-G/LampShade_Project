using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastracture.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastracture.EFCore;
public class InventoryContext : DbContext
{
    public DbSet<Inventory> Inventory { get; set; }
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(InventoryMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
