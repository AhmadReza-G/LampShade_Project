using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping;
public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Description).HasMaxLength(255);
        builder.Property(x => x.Picture).HasMaxLength(1000);
        builder.Property(x => x.PictureTitle).HasMaxLength(500);
        builder.Property(x => x.PictureAlt).HasMaxLength(255);
        builder.Property(x => x.Keywords).HasMaxLength(80);
        builder.Property(x => x.MetaDescription).HasMaxLength(255);
        builder.Property(x => x.Slug).HasMaxLength(300);
    }
}