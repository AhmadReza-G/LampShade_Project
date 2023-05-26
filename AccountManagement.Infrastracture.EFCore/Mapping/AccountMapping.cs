using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastracture.EFCore.Mapping;
public class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Fullname)
            .HasMaxLength(100);

        builder.Property(x => x.Username)
            .HasMaxLength(100);

        builder.Property(x => x.Password)
            .HasMaxLength(1000);

        builder.Property(x => x.ProfilePhoto)
            .HasMaxLength(500);

        builder.Property(x => x.Mobile)
            .HasMaxLength(20);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Accounts)
            .HasForeignKey(x => x.RoleId);
    }
}
