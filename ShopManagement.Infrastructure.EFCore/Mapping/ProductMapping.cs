using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping;
internal class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.ProductPictures)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.Name).HasMaxLength(255);
        builder.Property(x => x.Code).HasMaxLength(15);
        builder.Property(x => x.ShortDescription).HasMaxLength(500);
        builder.Property(x => x.Description).HasMaxLength(255);
        builder.Property(x => x.Picture).HasMaxLength(1000);
        builder.Property(x => x.PictureTitle).HasMaxLength(500);
        builder.Property(x => x.PictureAlt).HasMaxLength(255);
        builder.Property(x => x.Keywords).HasMaxLength(100);
        builder.Property(x => x.MetaDescription).HasMaxLength(255);
        builder.Property(x => x.Slug).HasMaxLength(500);
    }
}
