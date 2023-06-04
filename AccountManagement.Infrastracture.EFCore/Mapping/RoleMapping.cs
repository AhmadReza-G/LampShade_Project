using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping;
public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.HasMany(x => x.Accounts)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId);

        builder.OwnsMany(x => x.Permissions, navigationBuilder =>
        {
            navigationBuilder.ToTable("RolePermissions");
            navigationBuilder.HasKey(x => x.Id);
            navigationBuilder.WithOwner(x => x.Role).HasForeignKey(x => x.RoleId);
            navigationBuilder.Ignore(x => x.Name);
        });
    }
}
