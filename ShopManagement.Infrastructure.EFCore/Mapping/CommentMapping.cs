using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping;
public class CommentMapping : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ProductId);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x => x.Message).HasMaxLength(1000);
        builder.Property(x => x.Email).HasMaxLength(500);
    }
}
