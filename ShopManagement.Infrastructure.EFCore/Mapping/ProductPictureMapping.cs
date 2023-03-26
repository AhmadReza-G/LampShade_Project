using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping;
public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
{
    public void Configure(EntityTypeBuilder<ProductPicture> builder)
    {
        builder.ToTable("ProductPictures");
        builder.HasKey(p => p.Id);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductPictures)
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.Picture).HasMaxLength(1000);
        builder.Property(x => x.PictureTitle).HasMaxLength(500);
        builder.Property(x => x.PictureAlt).HasMaxLength(500);
    }
}
